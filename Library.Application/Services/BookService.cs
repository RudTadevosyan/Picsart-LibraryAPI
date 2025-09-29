using AutoMapper;
using Library.Application.Interfaces;
using Library.Domain.CustomExceptions;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Specifications;
using Library.Shared.CreationModels;
using Library.Shared.DTOs;
using Library.Shared.DTOs.FilterDtos;
using Library.Shared.UpdateModels;

namespace Library.Application.Services;

public class BookService: IBookService 
{
    private readonly IBookRepository _bookRepository;
    private readonly IBookDetailRepository _bookDetailRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public BookService(IBookRepository bookRepository, IBookDetailRepository bookDetailRepository, 
        IGenreRepository genreRepository, IMapper mapper, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _bookDetailRepository = bookDetailRepository;
        _genreRepository = genreRepository;
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<BookDto> GetBookById(int id)
    {
        var book = await _bookRepository.GetBookById(id) ??
            throw new NotFoundException("Book not found");
        return _mapper.Map<BookDto>(book);
    }

    public async Task<IEnumerable<BookDto>> GetAllBooks()
    {
        var books = await _bookRepository.GetAllBooks();
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksBySpec(BookFilterDto filter)
    {
        //Specification so we can assign AndSpecification to it
        Specification<Book> spec = new DefaultSpecification<Book>();
        
        if(!string.IsNullOrWhiteSpace(filter.BookTitle))
            spec = spec.And(new BookByTitleSpec(filter.BookTitle));
        
        if(!string.IsNullOrWhiteSpace(filter.AuthorName))
            spec = spec.And(new BookByAuthorSpec(filter.AuthorName));
        
        if(!string.IsNullOrWhiteSpace(filter.Genre))
            spec = spec.And(new BookByGenreSpec(filter.Genre));
        
        if(filter.PublishedAfter.HasValue)
            spec = spec.And(new BookByPublishedAfterSpec(filter.PublishedAfter));
        
        if(filter.IsAvailable.HasValue)
            spec = spec.And(new BookByAvailabilitySpec(filter.IsAvailable));
        
        var books = await _bookRepository.GetAllBooksBySpec(spec);

        //Sorting
        books = filter.SortBy switch 
        {
            "TitleAsc" => books.OrderBy(b => b.BookTitle),
            "TitleDesc" => books.OrderByDescending(b => b.BookTitle),
            "PublishDateAsc" => books.OrderBy(b => b.BookPublishDate),
            "PublishDateDesc" => books.OrderByDescending(b => b.BookPublishDate),
            _ => books
        };
        
        //Paging
        books = books.Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize);
        
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<BookDto> AddBook(CreateBookModel bookModel)
    {
        var book = _mapper.Map<Book>(bookModel);
        
        if(!await _authorRepository.CheckId(bookModel.AuthorId))
            throw new NotFoundException($"Author with id {bookModel.AuthorId} does not exist");

        if (book.BookDetail != null && book.BookDetail.BookPages <= 0)
            throw new DomainException("Book must have pages");
        
        foreach (var g in bookModel.Genres)
        {
            var genre = await _genreRepository.GetGenreById(g.GenreId);
            if(genre != null) book.Genres.Add(genre);
        }

        await _bookRepository.AddBook(book);
        await _bookRepository.SaveChanges();
        
        return _mapper.Map<BookDto>(book);
    }

    public async Task UpdateBook(int id, UpdateBookModel bookModel)
    {
        var existing = await _bookRepository.GetBookById(id) ??
            throw new NotFoundException("Book not found");
        
        if(!await _authorRepository.CheckId(bookModel.AuthorId))
            throw new NotFoundException($"Author with id {bookModel.AuthorId} does not exist");

        _mapper.Map(bookModel, existing);
        
        if(existing.BookDetail != null && existing.BookDetail.BookPages <= 0)
            throw new DomainException("Book must have pages");

        await _bookRepository.UpdateBook(existing);
        await _bookRepository.SaveChanges();
    }

    public async Task DeleteBook(int id)
    {
        var existing = await _bookRepository.GetBookById(id);
        if(existing == null)
            throw new NotFoundException("Book not found");

        await _bookRepository.DeleteBook(existing);
        await _bookRepository.SaveChanges();
    }

    public async Task AddGenreToBook(int bookId, int genreId)
    {
        var book = await _bookRepository.GetBookById(bookId) ??
                   throw new NotFoundException("Book not found");
        
        var genre = await _genreRepository.GetGenreById(genreId) ??
                    throw new NotFoundException("Genre not found");
        
        if(book.Genres.Contains(genre))
            throw new DomainException("Genre already added");

        book.Genres.Add(genre);
        await _bookRepository.SaveChanges();
    }

    public async Task DeleteGenreFromBook(int bookId, int genreId)
    {
        var book = await _bookRepository.GetBookById(bookId) ??
                   throw new NotFoundException("Book not found");
        var genre = await _genreRepository.GetGenreById(genreId) ?? 
                    throw new NotFoundException("Genre not found");
        
        if(!book.Genres.Contains(genre))
            throw new DomainException("Genre doesn't exist");

        book.Genres.Remove(genre);
        await _bookRepository.SaveChanges();
    }

    public async Task<bool> CheckBookAvailability(int bookId)
    {
        var book = await _bookRepository.GetBookById(bookId) ??
                   throw new NotFoundException("Book not found");
        
        return !book.Loans.Any(l => l.ReturnDate == null);
    }

    public async Task<BookDetailDto> AddBookDetail(int bookId, CreateBookDetailModel bookDetailModel)
    {
        var book = await _bookRepository.GetBookById(bookId) ??
                   throw new NotFoundException("Book not found");

        if(book.BookDetail != null)
            throw new DomainException("Book Detail already exists");
        
        var bd = _mapper.Map<BookDetail>(bookDetailModel);
        
        if(bd.BookPages <= 0)
            throw new DomainException("Book must have pages");

        book.BookDetail = bd;

        await _bookDetailRepository.AddBookDetail(bd);
        await _bookDetailRepository.SaveChanges();
        
        return _mapper.Map<BookDetailDto>(bd);
    }

    public async Task UpdateBookDetail(UpdateBookDetailModel bookDetailModel)
    {
        var bd = await _bookDetailRepository.GetBookDetailById(bookDetailModel.BookId) ??
                 throw new NotFoundException("Book Detail not found");

        if(bookDetailModel.BookPages <= 0)
            throw new DomainException("Book must have pages");
        
        _mapper.Map(bookDetailModel, bd);
        
        await _bookDetailRepository.UpdateBookDetail(bd);
        await _bookDetailRepository.SaveChanges();
    }

    public async Task DeleteBookDetail(int bookDetailId)
    {
        var bd = await _bookDetailRepository.GetBookDetailById(bookDetailId) ??
                 throw new NotFoundException("Book Detail not found");

        await _bookDetailRepository.DeleteBookDetail(bd);
        await _bookDetailRepository.SaveChanges();
    }
}

using AutoMapper;
using Library.Application.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Shared.CreationModels;
using Library.Shared.DTOs;
using Library.Shared.UpdateModels;

namespace Library.Application.Services;

public class BookService: IBookService 
{
    private readonly IBookRepository _bookRepository;
    private readonly IBookDetailRepository _bookDetailRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public BookService(IBookRepository bookRepository, IBookDetailRepository bookDetailRepository, 
        IGenreRepository genreRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _bookDetailRepository = bookDetailRepository;
        _genreRepository = genreRepository;
        _mapper = mapper;
    }

    public async Task<BookDto?> GetBookById(int id)
    {
        var book = await _bookRepository.GetBookById(id);
        return _mapper.Map<BookDto?>(book);
    }

    public async Task<IEnumerable<BookDto>> GetAllBooks()
    {
        var books = await _bookRepository.GetAllBooks();
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<BookDto?> AddBook(CreateBookModel bookModel)
    {
        var book = _mapper.Map<Book>(bookModel);

        if (bookModel.Genres != null)
        {
            foreach (var g in bookModel.Genres)
            {
                var genre = await _genreRepository.GetGenreById(g.GenreId);
                if(genre != null) book.Genres.Add(genre);
            }
        }
        //add Page check

        await _bookRepository.AddBook(book);
        await _bookRepository.Save();
        
        return _mapper.Map<BookDto?>(book);
    }

    public async Task<bool> UpdateBook(int id, UpdateBookModel bookModel)
    {
        var existing = await _bookRepository.GetBookById(id);
        if(existing == null) return false;

        _mapper.Map(bookModel, existing);
        //add Page check
        await _bookRepository.UpdateBook(existing);
        await _bookRepository.Save();
        return true;
    }

    public async Task<bool> DeleteBook(int id)
    {
        var existing = await _bookRepository.GetBookById(id);
        if(existing == null) return false;

        await _bookRepository.DeleteBook(id);
        await _bookRepository.Save();
        return true;
    }

    public async Task<bool> AddGenreToBook(int bookId, int genreId)
    {
        var genre = await _genreRepository.GetGenreById(genreId);
        var book = await _bookRepository.GetBookById(bookId);
        if (genre == null || book == null) return false;

        book.Genres.Add(genre);
        await _bookRepository.Save();
        return true;
    }

    public async Task<bool> DeleteGenreFromBook(int bookId, int genreId)
    {
        var genre = await _genreRepository.GetGenreById(genreId);
        var book = await _bookRepository.GetBookById(bookId);
        if (genre == null || book == null) return false;

        book.Genres.Remove(genre);
        await _bookRepository.Save();
        return true;
    }

    public async Task<bool> CheckBookAvailability(int bookId)
    {
        var book = await _bookRepository.GetBookById(bookId);
        if (book == null) return false;
        
        return !book.Loans.Any(l => l.ReturnDate == null);
    }

    public async Task<BookDetailDto?> AddBookDetail(int bookId, CreateBookDetailModel bookDetailModel)
    {
        var book = await _bookRepository.GetBookById(bookId);
        if (book == null) return null;

        var bd = _mapper.Map<BookDetail>(bookDetailModel);

        book.BookDetail = bd;

        await _bookDetailRepository.AddBookDetail(bd);
        await _bookDetailRepository.Save();
        
        return _mapper.Map<BookDetailDto?>(bd);
    }

    public async Task<bool> UpdateBookDetail(UpdateBookDetailModel bookDetailModel)
    {
        var bd = await _bookDetailRepository.GetBookDetailById(bookDetailModel.BookId);
        if (bd == null) return false; 

        _mapper.Map(bookDetailModel, bd);

        await _bookDetailRepository.UpdateBookDetail(bd);
        await _bookDetailRepository.Save();
        return true;
    }

    public async Task<bool> DeleteBookDetail(int bookDetailId)
    {
        var bd = await _bookDetailRepository.GetBookDetailById(bookDetailId);
        if (bd == null) return false;

        await _bookDetailRepository.DeleteBookDetail(bookDetailId);
        await _bookDetailRepository.Save();
        return true;
    }
}

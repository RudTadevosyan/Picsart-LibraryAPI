using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;

    public BookRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<Book?> GetBookById(int id)
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.BookDetail)
            .Include(b => b.Genres)
            .Include(b => b.Loans)
            .FirstOrDefaultAsync(b => b.BookId == id);
    }

    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.BookDetail)
            .Include(b => b.Genres)
            .Include(b => b.Loans)
            .ToListAsync();
    }

    public async Task AddBook(Book book)
    {
        await _context.Books.AddAsync(book);
    }

    public Task<bool> UpdateBook(Book book)
    {
        _context.Books.Update(book);
        return Task.FromResult(true);
    }

    public async Task<bool> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return false;

        _context.Books.Remove(book);
        return true;
    }

    public async Task AddGenreToBook(int bookId, int genreId)
    {
        var book = await _context.Books
            .Include(b => b.Genres)
            .FirstOrDefaultAsync(b => b.BookId == bookId);

        var genre = await _context.Genres.FindAsync(genreId);

        if (book != null && genre != null && !book.Genres.Contains(genre))
        {
            book.Genres.Add(genre);
        }
    }

    public async Task RemoveGenreFromBook(int bookId, int genreId)
    {
        var book = await _context.Books
            .Include(b => b.Genres)
            .FirstOrDefaultAsync(b => b.BookId == bookId);

        var genre = await _context.Genres.FindAsync(genreId);

        if (book != null && genre != null && book.Genres.Contains(genre))
        {
            book.Genres.Remove(genre);
        }
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}

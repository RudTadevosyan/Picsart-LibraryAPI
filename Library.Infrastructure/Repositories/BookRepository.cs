using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class BookRepository : BaseRepository<Book>,IBookRepository
{
    public BookRepository(LibraryDbContext context) : base(context) {}

    public async Task<Book?> GetBookById(int id)
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.BookDetail)
            .Include(b => b.Genres)
            .Include(b => b.Loans)
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.BookId == id);
    }

    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.BookDetail)
            .Include(b => b.Genres)
            .Include(b => b.Loans)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Book>> GetAllBooksBySpec(Specification<Book> spec)
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.BookDetail)
            .Include(b => b.Genres)
            .Include(b => b.Loans)
            .AsNoTracking()
            .Where(spec.ToExpression())
            .ToListAsync();
    }

    public async Task AddBook(Book book)
    {
        await _context.Books.AddAsync(book);
    }

    public Task UpdateBook(Book book)
    {
        _context.Books.Update(book);
        return Task.FromResult(true);
    }

    public Task DeleteBook(Book book)
    {
        _context.Books.Remove(book);
        return Task.CompletedTask;
    }
}

using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class BookDetailRepository : BaseRepository<BookDetail> ,IBookDetailRepository
{
    public BookDetailRepository(LibraryDbContext context) : base (context) {}

    public async Task<BookDetail?> GetBookDetailById(int id)
    {
        return await _context.BookDetails
            .Include(d => d.Book)
            .ThenInclude(b => b.Author)
            .FirstOrDefaultAsync(d => d.BookDetailsId == id);
    }

    public async Task<IEnumerable<BookDetail>> GetAllBookDetails()
    {
        return await _context.BookDetails
            .Include(d => d.Book)
            .ThenInclude(b => b.Author)
            .ToListAsync();
    }

    public async Task AddBookDetail(BookDetail bookDetail)
    {
        await _context.BookDetails.AddAsync(bookDetail);
    }

    public Task UpdateBookDetail(BookDetail bookDetail)
    {
        _context.BookDetails.Update(bookDetail);
        return Task.CompletedTask;
    }

    public Task DeleteBookDetail(BookDetail bookDetail)
    {
        _context.BookDetails.Remove(bookDetail);
        return Task.CompletedTask;
    }
}
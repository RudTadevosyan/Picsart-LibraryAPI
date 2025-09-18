using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class BookDetailRepository : IBookDetailRepository
{
    private readonly LibraryDbContext _context;

    public BookDetailRepository(LibraryDbContext context)
    {
        _context = context;
    }

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

    public Task<bool> UpdateBookDetail(BookDetail bookDetail)
    {
        _context.BookDetails.Update(bookDetail);
        return Task.FromResult(true);
    }

    public async Task<bool> DeleteBookDetail(int id)
    {
        var detail = await _context.BookDetails.FindAsync(id);
        if (detail == null) return false;

        _context.BookDetails.Remove(detail);
        return true;
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
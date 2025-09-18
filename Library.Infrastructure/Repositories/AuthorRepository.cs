using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _context;

    public AuthorRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<Author?> GetAuthorById(int id)
    {
        return await _context.Authors
            .Include(a => a.Books)!
            .ThenInclude(b => b.BookDetail)
            .Include(a => a.Books)
            .ThenInclude(b => b.Genres)
            .Include(a => a.Books)
            .ThenInclude(b => b.Loans)
            .FirstOrDefaultAsync(a => a.AuthorId == id);
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await _context.Authors
            .Include(a => a.Books)!
            .ThenInclude(b => b.BookDetail)
            .Include(a => a.Books)
            .ThenInclude(b => b.Genres)
            .Include(a => a.Books)
            .ThenInclude(b => b.Loans)
            .ToListAsync();
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsByName(string name)
    {
        return await _context.Authors
            .Where(a => a.AuthorName.StartsWith(name))
            .Include(a => a.Books)!
            .ThenInclude(b => b.BookDetail)
            .Include(a => a.Books)
            .ThenInclude(b => b.Genres)
            .Include(a => a.Books)
            .ThenInclude(b => b.Loans)
            .ToListAsync();
    }

    public async Task AddAuthor(Author author)
    {
        await _context.Authors.AddAsync(author);
    }

    public Task<bool> UpdateAuthor(Author author)
    {
        _context.Authors.Update(author);
        return Task.FromResult(true);
    }

    public async Task<bool> DeleteAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null) return false;

        _context.Authors.Remove(author);
        return true;
    }

    public async Task<bool> AuthorHasBooks(int authorId)
    {
        return await _context.Books.AnyAsync(b => b.AuthorId == authorId);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
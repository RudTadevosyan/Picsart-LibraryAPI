using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
{
    public AuthorRepository(LibraryDbContext context) : base(context){}

    public async Task<Author?> GetAuthorById(int id)
    {
        return await _context.Authors
            .Include(a => a.Books)
            .ThenInclude(b => b.BookDetail)
            .Include(a => a.Books)
            .ThenInclude(b => b.Genres)
            .Include(a => a.Books)
            .ThenInclude(b => b.Loans)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AuthorId == id);
    }

    public async Task<IEnumerable<Author>> GetAllAuthors()
    {
        return await _context.Authors
            .Include(a => a.Books)
            .ThenInclude(b => b.BookDetail)
            .Include(a => a.Books)
            .ThenInclude(b => b.Genres)
            .Include(a => a.Books)
            .ThenInclude(b => b.Loans)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsByName(string name)
    {
        return await _context.Authors
            .Where(a => a.AuthorName.StartsWith(name))
            .Include(a => a.Books)
            .ThenInclude(b => b.BookDetail)
            .Include(a => a.Books)
            .ThenInclude(b => b.Genres)
            .Include(a => a.Books)
            .ThenInclude(b => b.Loans)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAuthor(Author author)
    {
        await _context.Authors.AddAsync(author);
    }

    public Task UpdateAuthor(Author author)
    {
        _context.Authors.Update(author);
        return Task.FromResult(true);
    }

    public Task DeleteAuthor(Author author)
    {
        _context.Authors.Remove(author);
        return Task.CompletedTask;
    }

    public async Task<bool> AuthorHasBooks(int authorId)
    {
        return await _context.Books.AnyAsync(b => b.AuthorId == authorId);
    }

}
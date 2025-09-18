using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly LibraryDbContext _context;

    public GenreRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<Genre?> GetGenreById(int id)
    {
        return await _context.Genres
            .Include(g => g.Books)
            .ThenInclude(b => b.BookDetail)
            .FirstOrDefaultAsync(g => g.GenreId == id);
    }

    public async Task<IEnumerable<Genre>> GetAllGenres()
    {
        return await _context.Genres
            .Include(g => g.Books)
            .ToListAsync();
    }

    public async Task AddGenre(Genre genre)
    {
        await _context.Genres.AddAsync(genre);
    }

    public Task<bool> UpdateGenre(Genre genre)
    {
        _context.Genres.Update(genre);
        return Task.FromResult(true);
    }

    public async Task<bool> DeleteGenreById(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null) return false;

        _context.Genres.Remove(genre);
        return true;
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
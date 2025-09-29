using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class GenreRepository : BaseRepository<Genre>,IGenreRepository
{
    public GenreRepository(LibraryDbContext context) : base(context) {}

    public async Task<Genre?> GetGenreById(int id)
    {
        return await _context.Genres
            .Include(g => g.Books)
            .ThenInclude(b => b.BookDetail)
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.GenreId == id);
    }

    public async Task<IEnumerable<Genre>> GetAllGenres()
    {
        return await _context.Genres
            .Include(g => g.Books)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddGenre(Genre genre)
    {
        await _context.Genres.AddAsync(genre);
    }

    public Task UpdateGenre(Genre genre)
    {
        _context.Genres.Update(genre);
        return Task.CompletedTask;
    }

    public Task DeleteGenreById(Genre genre)
    {
        _context.Genres.Remove(genre);
        return Task.CompletedTask;
    }
}
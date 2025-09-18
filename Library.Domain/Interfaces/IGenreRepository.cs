using Library.Domain.Models;

namespace Library.Domain.Interfaces;

public interface IGenreRepository
{
    Task<Genre?> GetGenreById(int id);
    Task<IEnumerable<Genre>> GetAllGenres();
    Task AddGenre(Genre genre);
    Task<bool> UpdateGenre(Genre genre);
    Task<bool> DeleteGenreById(int id);
    Task Save();
}
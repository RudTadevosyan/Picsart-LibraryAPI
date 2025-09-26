using Library.Domain.Models;

namespace Library.Domain.Interfaces;

public interface IGenreRepository : IRepository<Genre>
{
    Task<Genre?> GetGenreById(int id);
    Task<IEnumerable<Genre>> GetAllGenres();
    Task AddGenre(Genre genre);
    Task UpdateGenre(Genre genre);
    Task DeleteGenreById(Genre genre);
}
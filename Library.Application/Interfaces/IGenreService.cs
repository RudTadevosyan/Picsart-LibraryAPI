using Library.Shared.DTOs.Genre;

namespace Library.Application.Interfaces
{
    public interface IGenreService
    {
        Task<GenreDto> GetGenreById(int id);
        Task<IEnumerable<GenreDto>> GetAllGenres();
        Task<GenreDto> AddGenre(CreateGenreModel genreModel);
        Task UpdateGenre(int id, UpdateGenreModel genreModel);
        Task DeleteGenre(int id);
    }
}
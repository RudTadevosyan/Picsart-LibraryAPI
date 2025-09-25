using Library.Shared.CreationModels;
using Library.Shared.DTOs;
using Library.Shared.UpdateModels;

namespace Library.Application.Interfaces
{
    public interface IGenreService
    {
        Task<GenreDto?> GetGenreById(int id);
        Task<IEnumerable<GenreDto>> GetAllGenres();
        Task<GenreDto?> AddGenre(CreateGenreModel genreModel);
        Task<bool> UpdateGenre(int id, UpdateGenreModel genreModel);
        Task<bool> DeleteGenre(int id);
    }
}
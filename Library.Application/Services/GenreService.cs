using Library.Application.Helpers;
using Library.Application.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Shared.CreationModels;
using Library.Shared.DTOs;
using Library.Shared.UpdateModels;

namespace Library.Application.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _repository;

    public GenreService(IGenreRepository repository)
    {
        _repository = repository;
    }

    public async Task<GenreDto?> GetGenreById(int id)
    {
        var genre = await _repository.GetGenreById(id);
        return genre.ToDto();
    }

    public async Task<IEnumerable<GenreDto?>> GetAllGenres()
    {
        var genres = await _repository.GetAllGenres();
        return genres.Select(g => g.ToDto());
    }

    public async Task<GenreDto?> AddGenre(CreateGenreModel genreModel)
    {
        Genre genre = new Genre
        {
            GenreName = genreModel.GenreName,
        };

        await _repository.AddGenre(genre);
        await _repository.Save();
        return genre.ToDto();
    }

    public async Task<bool> UpdateGenre(int id, UpdateGenreModel genreModel)
    {
        var existing = await _repository.GetGenreById(id);
        if(existing == null) return false;

        if(!string.IsNullOrWhiteSpace(genreModel.GenreName))
            existing.GenreName = genreModel.GenreName;

        await _repository.UpdateGenre(existing);
        await _repository.Save();
        return true;
    }

    public async Task<bool> DeleteGenre(int id)
    {
        var existing = await _repository.GetGenreById(id);
        if(existing == null) return false;

        await _repository.DeleteGenreById(id);
        await _repository.Save();
        return true;
    }
}
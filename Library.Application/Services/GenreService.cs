using AutoMapper;
using Library.Application.Interfaces;
using Library.Domain.CustomExceptions;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Shared.DTOs.Genre;

namespace Library.Application.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _repository;
    private readonly IMapper _mapper;
    public GenreService(IGenreRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GenreDto> GetGenreById(int id)
    {
        var genre = await _repository.GetGenreById(id) ??
                    throw new NotFoundException("Genre not found");
        
        return _mapper.Map<GenreDto>(genre);
    }

    public async Task<IEnumerable<GenreDto>> GetAllGenres()
    {
        var genres = await _repository.GetAllGenres();
        return _mapper.Map<IEnumerable<GenreDto>>(genres);
    }

    public async Task<GenreDto> AddGenre(CreateGenreModel genreModel)
    {
        Genre genre = _mapper.Map<Genre>(genreModel);

        await _repository.AddGenre(genre);
        await _repository.SaveChanges();
        
        return _mapper.Map<GenreDto>(genre);
    }

    public async Task UpdateGenre(int id, UpdateGenreModel genreModel)
    {
        var existing = await _repository.GetGenreById(id) ??
                       throw new NotFoundException("Genre not found");
        
        if(!string.IsNullOrWhiteSpace(genreModel.GenreName))
            existing.GenreName = genreModel.GenreName;

        await _repository.UpdateGenre(existing);
        await _repository.SaveChanges();
    }

    public async Task DeleteGenre(int id)
    {
        var genre = await _repository.GetGenreById(id) ?? 
                       throw new NotFoundException("Genre not found");

        await _repository.DeleteGenreById(genre);
        await _repository.SaveChanges();
    }
}
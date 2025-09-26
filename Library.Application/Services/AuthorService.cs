using AutoMapper;
using Library.Application.Interfaces;
using Library.Domain.CustomExceptions;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Shared.CreationModels;
using Library.Shared.DTOs;
using Library.Shared.UpdateModels;

namespace Library.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;

    public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
    {
        _repository = authorRepository;
        _mapper = mapper;
    }

    public async Task<AuthorDto> GetAuthorById(int id)
    {
        var author = await _repository.GetAuthorById(id) ??
                     throw new NotFoundException("Author not found");
        return _mapper.Map<AuthorDto>(author);
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAuthors()
    {
        var authors = await _repository.GetAllAuthors();
        return _mapper.Map<IEnumerable<AuthorDto>>(authors);
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAuthorsByName(string name)
    {
        var authors = await _repository.GetAllAuthorsByName(name);
        return _mapper.Map<IEnumerable<AuthorDto>>(authors);
    }
    public async Task<AuthorDto> AddAuthor(CreateAuthorModel authorModel)
    {
        var author = _mapper.Map<Author>(authorModel);
        
        await _repository.AddAuthor(author);
        await _repository.SaveChanges();
        
        return _mapper.Map<AuthorDto>(author);
    }

    public async Task UpdateAuthor(int id, UpdateAuthorModel authorModel)
    {
        var existing = await _repository.GetAuthorById(id) ??
                       throw new NotFoundException("Author not found");
        
        _mapper.Map(authorModel, existing);
        
        await _repository.UpdateAuthor(existing);
        await _repository.SaveChanges();
    }

    public async Task DeleteAuthor(int id)
    {
        var existing = await _repository.GetAuthorById(id) ??
                       throw new NotFoundException("Author not found");
        
        if(await _repository.AuthorHasBooks(id))
            throw new DomainException("Cannot delete author with books");

        await _repository.DeleteAuthor(existing);
        await _repository.SaveChanges();
    }
}
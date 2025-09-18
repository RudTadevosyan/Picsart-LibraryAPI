using Library.Application.Helpers;
using Library.Application.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Shared.CreationModels;
using Library.Shared.DTOs;
using Library.Shared.UpdateModels;

namespace Library.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _repository = authorRepository;
    }

    public async Task<AuthorDto?> GetAuthorById(int id)
    {
        var author = await _repository.GetAuthorById(id);
        return author.ToDto();
    }

    public async Task<IEnumerable<AuthorDto?>> GetAllAuthors()
    {
        var authors = await _repository.GetAllAuthors();
        return authors.Select(a => a.ToDto());
    }

    public async Task<IEnumerable<AuthorDto?>> GetAllAuthorsByName(string name)
    {
        var authors = await _repository.GetAllAuthorsByName(name);
        return authors.Select(a => a.ToDto());
    }
    public async Task<AuthorDto?> AddAuthor(CreateAuthorModel authorModel)
    {
        Author author = new Author
        {
            AuthorName = authorModel.AuthorName,
            AuthorEmail = authorModel.AuthorEmail,
        };

        await _repository.AddAuthor(author);
        await _repository.Save();
        return author.ToDto();
    }

    public async Task<bool> UpdateAuthor(int id, UpdateAuthorModel authorModel)
    {
        var existing = await _repository.GetAuthorById(id);
        if(existing == null) return false;

        if (!string.IsNullOrWhiteSpace(authorModel.AuthorName))
            existing.AuthorName = authorModel.AuthorName;
        
        if(!string.IsNullOrWhiteSpace(authorModel.AuthorEmail))
            existing.AuthorEmail = authorModel.AuthorEmail;
        
        await _repository.UpdateAuthor(existing);
        await _repository.Save();
        return true;
    }

    public async Task<bool> DeleteAuthor(int id)
    {
        var existing = await _repository.GetAuthorById(id);
        if (existing == null)
            return false;
        if(await _repository.AuthorHasBooks(id))
            throw new InvalidOperationException("Cannot delete author with book");

        await _repository.DeleteAuthor(id);
        await _repository.Save();
        return true;
    }
}
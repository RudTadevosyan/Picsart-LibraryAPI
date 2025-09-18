using Library.Domain.Models;

namespace Library.Domain.Interfaces;

public interface IAuthorRepository
{
    Task<Author?> GetAuthorById(int id);
    
    Task<IEnumerable<Author>> GetAllAuthorsByName(string name);
    Task<IEnumerable<Author>> GetAllAuthors();
    Task AddAuthor(Author author);
    Task<bool> UpdateAuthor(Author author);
    Task<bool> DeleteAuthor(int id);
    Task<bool> AuthorHasBooks(int authorId);
    
    Task Save();
}
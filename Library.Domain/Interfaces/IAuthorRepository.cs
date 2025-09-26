using Library.Domain.Models;

namespace Library.Domain.Interfaces;

public interface IAuthorRepository : IRepository<Author>
{
    Task<Author?> GetAuthorById(int id);
    Task<IEnumerable<Author>> GetAllAuthorsByName(string name);
    Task<IEnumerable<Author>> GetAllAuthors();
    Task AddAuthor(Author author);
    Task UpdateAuthor(Author author);
    Task DeleteAuthor(Author author);
    Task<bool> AuthorHasBooks(int authorId);
}
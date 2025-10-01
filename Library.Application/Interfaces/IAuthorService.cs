using Library.Shared.DTOs.Author;

namespace Library.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorDto> GetAuthorById(int id);
        Task<IEnumerable<AuthorDto>> GetAllAuthors();
        Task<IEnumerable<AuthorDto>> GetAllAuthorsByName(string name);
        Task<AuthorDto> AddAuthor(CreateAuthorModel authorModel);
        Task UpdateAuthor(int id, UpdateAuthorModel authorModel);
        Task DeleteAuthor(int id);
    }
}
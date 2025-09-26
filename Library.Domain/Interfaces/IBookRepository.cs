using Library.Domain.Models;

namespace Library.Domain.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    Task<Book?> GetBookById(int id);
    Task<IEnumerable<Book>> GetAllBooks();
    Task AddBook(Book book);
    Task UpdateBook(Book book);
    Task DeleteBook(Book book);
}
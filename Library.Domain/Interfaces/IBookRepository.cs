using Library.Domain.Models;
using Library.Domain.Specifications;

namespace Library.Domain.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    Task<Book?> GetBookById(int id);
    Task<IEnumerable<Book>> GetAllBooks();
    Task<IEnumerable<Book>> GetAllBooksBySpec(Specification<Book> spec);
    Task AddBook(Book book);
    Task UpdateBook(Book book);
    Task DeleteBook(Book book);
}
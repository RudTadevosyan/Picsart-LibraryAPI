using Library.Domain.Models;

namespace Library.Domain.Interfaces;

public interface IBookRepository
{
    Task<Book?> GetBookById(int id);
    Task<IEnumerable<Book>> GetAllBooks();
    Task AddBook(Book book);
    Task<bool> UpdateBook(Book book);
    Task<bool> DeleteBook(int id);
    
    Task AddGenreToBook(int bookId, int genreId);
    Task RemoveGenreFromBook(int bookId, int genreId);
    Task Save(); 
}
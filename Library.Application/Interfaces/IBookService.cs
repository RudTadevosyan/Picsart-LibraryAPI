using Library.Shared.CreationModels;
using Library.Shared.DTOs;
using Library.Shared.UpdateModels;

namespace Library.Application.Interfaces;

public interface IBookService
{
    Task<BookDto?> GetBookById(int id);
    Task<IEnumerable<BookDto?>> GetAllBooks();
    Task<BookDto?> AddBook(CreateBookModel bookModel);
    Task<bool> UpdateBook(int id, UpdateBookModel bookModel);
    Task<bool> DeleteBook(int id);
    
    Task<bool> AddGenreToBook(int bookId, int genreId);
    Task<bool> DeleteGenreFromBook(int bookId, int genreId);
    Task<bool> CheckBookAvailability(int bookId);
    
    Task<BookDetailDto?> AddBookDetail(int bookId, CreateBookDetailModel bookDetailModel);
    Task<bool> UpdateBookDetail(UpdateBookDetailModel bookDetailModel);
    Task<bool> DeleteBookDetail(int bookDetailId);
}
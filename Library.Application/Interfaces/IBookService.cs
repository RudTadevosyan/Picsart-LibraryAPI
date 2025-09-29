using Library.Domain.Models;
using Library.Domain.Specifications;
using Library.Shared.CreationModels;
using Library.Shared.DTOs;
using Library.Shared.DTOs.FilterDtos;
using Library.Shared.UpdateModels;

namespace Library.Application.Interfaces;

public interface IBookService
{
    Task<BookDto> GetBookById(int id);
    Task<IEnumerable<BookDto>> GetAllBooks();
    Task<IEnumerable<BookDto>> GetAllBooksBySpec(BookFilterDto filter);
    Task<BookDto> AddBook(CreateBookModel bookModel);
    Task UpdateBook(int id, UpdateBookModel bookModel);
    Task DeleteBook(int id);
    
    Task AddGenreToBook(int bookId, int genreId);
    Task DeleteGenreFromBook(int bookId, int genreId);
    Task<bool> CheckBookAvailability(int bookId);
    
    Task<BookDetailDto> AddBookDetail(int bookId, CreateBookDetailModel bookDetailModel);
    Task UpdateBookDetail(UpdateBookDetailModel bookDetailModel);
    Task DeleteBookDetail(int bookDetailId);
}
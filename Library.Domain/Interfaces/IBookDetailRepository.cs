using Library.Domain.Models;

namespace Library.Domain.Interfaces;

public interface IBookDetailRepository
{
    Task<BookDetail?> GetBookDetailById(int bookDetailId);
    Task<IEnumerable<BookDetail>> GetAllBookDetails();
    Task AddBookDetail(BookDetail bookDetail);
    Task<bool> UpdateBookDetail(BookDetail bookDetail);
    Task<bool> DeleteBookDetail(int id);
    Task Save();
}
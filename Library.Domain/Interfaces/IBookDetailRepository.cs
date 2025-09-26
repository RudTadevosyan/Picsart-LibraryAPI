using Library.Domain.Models;

namespace Library.Domain.Interfaces;

public interface IBookDetailRepository : IRepository<BookDetail>
{
    Task<BookDetail?> GetBookDetailById(int bookDetailId);
    Task<IEnumerable<BookDetail>> GetAllBookDetails();
    Task AddBookDetail(BookDetail bookDetail);
    Task UpdateBookDetail(BookDetail bookDetail);
    Task DeleteBookDetail(BookDetail bookDetail);
}
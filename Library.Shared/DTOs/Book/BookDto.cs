using Library.Shared.DTOs.BookDetail;
using Library.Shared.DTOs.Genre;

namespace Library.Shared.DTOs.Book;

public class BookDto
{
    public int BookId { get; set; }  
    public string BookTitle { get; set; } = string.Empty;
    public DateTime BookPublishDate { get; set; }
    
    public int AuthorId { get; set; }
    public ICollection<GenreDto> Genres { get; set; } = new List<GenreDto>();
    public BookDetailDto? BookDetail { get; set; }
    
    public int? ActiveLoanId { get; set; }
    
}
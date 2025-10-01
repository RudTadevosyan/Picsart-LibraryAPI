using Library.Shared.DTOs.BookDetail;
using Library.Shared.DTOs.Genre;

namespace Library.Shared.DTOs.Book;

public class CreateBookModel
{
    public string BookTitle { get; set; } = string.Empty;
    public DateTime BookPublishDate { get; set; }
    public int AuthorId { get; set; }
    
    //not required 
    public CreateBookDetailModel? BookDetail { get; set; }
    public ICollection<GenreDto> Genres { get; set; } = new List<GenreDto>();
}
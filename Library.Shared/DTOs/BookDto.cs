namespace Library.Shared.DTOs;

public class BookDto
{
    public int BookId { get; set; }  
    public string BookTitle { get; set; } = string.Empty;
    public DateTime BookPublishDate { get; set; }
    
    public int AuthorId { get; set; }
    public ICollection<GenreDto>? Genres { get; set; } 
    public BookDetailDto? BookDetail { get; set; }
    
    public int? LoanId { get; set; }
    
}
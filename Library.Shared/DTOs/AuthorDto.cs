namespace Library.Shared.DTOs;

public class AuthorDto
{
    public int AuthorId { get; set; } 
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorEmail { get; set; } = string.Empty;
    public ICollection<BookDto> Books { get; set; } = new List<BookDto>();
}
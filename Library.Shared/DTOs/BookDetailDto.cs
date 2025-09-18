namespace Library.Shared.DTOs;
public class BookDetailDto
{
    public int BookPages { get; set; }
    public string BookLanguage { get; set; } = string.Empty;
    public string? BookDescription { get; set; }
}
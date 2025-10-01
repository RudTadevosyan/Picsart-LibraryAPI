namespace Library.Shared.DTOs.BookDetail;
public class BookDetailDto
{
    public int BookDetailsId { get; set; }
    public int BookPages { get; set; }
    public string BookLanguage { get; set; } = string.Empty;
    public string? BookDescription { get; set; }
}
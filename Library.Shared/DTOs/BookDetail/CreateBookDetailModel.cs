namespace Library.Shared.DTOs.BookDetail;

public class CreateBookDetailModel
{
    public int BookPages { get; set; }
    public string BookLanguage { get; set; } = string.Empty;
    public string? BookDescription { get; set; }
}
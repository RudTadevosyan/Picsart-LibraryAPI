using Library.Shared.DTOs;

namespace Library.Shared.CreationModels;

public class CreateBookModel
{
    public string BookTitle { get; set; } = string.Empty;
    public DateTime BookPublishDate { get; set; }
    public int AuthorId { get; set; }
    
    //not required 
    public CreateBookDetailModel? BookDetail { get; set; }
    public ICollection<GenreDto>? Genres { get; set; } //idk
}
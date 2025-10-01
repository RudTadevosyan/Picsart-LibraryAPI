namespace Library.Shared.DTOs.Book
{
    public class UpdateBookModel
    {
        public string? BookTitle { get; set; }
        public DateTime BookPublishDate { get; set; }
        public int AuthorId { get; set; }
    }
}
namespace Library.Shared.UpdateModels
{
    public class UpdateBookModel
    {
        public string? BookTitle { get; set; }
        public DateTime BookPublishDate { get; set; }
        public int AuthorId { get; set; }
    }
}
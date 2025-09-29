namespace Library.Shared.UpdateModels
{
    public class UpdateBookDetailModel
    {
        public int BookId { get; set; } 
        public int BookPages { get; set; }
        public string? BookLanguage { get; set; }
        public string? BookDescription { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Models;
//BookDetails -> Book 1:1
public class BookDetail
{
    [Key]
    public int BookDetailsId { get; set; }
    
    //FK
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    
    [Range(1, 100000)]
    public int BookPages { get; set; }
    
    [Required, MaxLength(50)]
    public string BookLanguage { get; set; } = string.Empty;
    public string? BookDescription { get; set; }
}
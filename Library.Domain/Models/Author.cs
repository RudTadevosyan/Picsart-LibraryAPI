using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Models;

//Book -> Author 1:N
public class Author
{   
    [Key]
    public int AuthorId { get; set; }
    
    [Required, MaxLength(50)]
    public string AuthorName { get; set; } = string.Empty;
    
    [Required, MaxLength(50)]
    public string AuthorEmail { get; set; } = string.Empty;
    public ICollection<Book>? Books { get; set; } 
}
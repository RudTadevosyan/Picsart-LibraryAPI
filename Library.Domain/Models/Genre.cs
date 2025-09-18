using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Models;
//Genre -> Book N:M
public class Genre
{
    [Key]
    public int GenreId { get; set; }
    
    [Required, MaxLength(50)]
    public string GenreName { get; set; } = string.Empty;
    
    public ICollection<Book> Books { get; set; } = new HashSet<Book>();
}
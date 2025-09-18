using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Models;
//Book -> Author 1:N
//Book -> BookDetails 1:1
//Book -> Genres N:M
//Book -> Loan 1:N (not in the same time)
public class Book
{
    [Key]
    public int BookId { get; set; }
    
    [Required, MaxLength(50)]
    public string BookTitle { get; set; } = String.Empty;
    
    public DateTime BookPublishDate{ get; set; }
    
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
    
    public ICollection<Genre> Genres { get; set; } = new HashSet<Genre>(); 
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    public BookDetail? BookDetail { get; set; } //nullable

}
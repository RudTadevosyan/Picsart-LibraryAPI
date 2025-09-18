using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Models;
//Loan -> Member 1:N
public class Loan
{
    [Key]
    public int LoanId { get; set; }
    
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    
    //FK
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    
    //FK
    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;
    
}
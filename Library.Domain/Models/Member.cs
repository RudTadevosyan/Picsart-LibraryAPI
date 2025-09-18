using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Models;

public class Member
{
    [Key]
    public int MemberId { get; set; }
    
    [Required, MaxLength(50)]
    public string MemberName { get; set; } = string.Empty;
    
    [Required]
    public string MemberEmail { get; set; } = string.Empty;

    public ICollection<Loan> Loans { get; set; } = new List<Loan>(); 
}
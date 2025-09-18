namespace Library.Shared.DTOs;

public class MemberDto
{
    public int MemberId { get; set; }  
    public string MemberName { get; set; } = string.Empty;
    public string MemberEmail { get; set; } = string.Empty;
    public ICollection<BookDto>? LoanedBooks { get; set; } = new List<BookDto>();
}
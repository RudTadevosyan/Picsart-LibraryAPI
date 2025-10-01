using Library.Shared.DTOs.Book;

namespace Library.Shared.DTOs.Member;

public class MemberDto
{
    public int MemberId { get; set; }  
    public string MemberName { get; set; } = string.Empty;
    public string MemberEmail { get; set; } = string.Empty;
    public ICollection<BookDto> LoanedBooks { get; set; } = new List<BookDto>();
}
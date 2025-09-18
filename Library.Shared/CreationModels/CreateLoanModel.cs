namespace Library.Shared.CreationModels;

public class CreateLoanModel
{
    public DateTime LoanDate { get; set; }
    public int BookId { get; set; }
    public int MemberId { get; set; }
}
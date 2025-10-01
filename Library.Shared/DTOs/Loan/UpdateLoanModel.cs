namespace Library.Shared.DTOs.Loan
{
    public class UpdateLoanModel
    {
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; } 
        public int BookId { get; set; }
        public int MemberId { get; set; }
    }
}
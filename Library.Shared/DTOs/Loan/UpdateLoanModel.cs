namespace Library.Shared.UpdateModels
{
    public class UpdateLoanModel
    {
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; } 
        public int BookId { get; set; }
        public int MemberId { get; set; }
    }
}
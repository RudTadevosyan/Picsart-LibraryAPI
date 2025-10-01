using Library.Shared.DTOs.Loan;

namespace Library.Application.Interfaces
{
    public interface ILoanService
    {
        Task<LoanDto> GetLoanById(int id);
        Task<IEnumerable<LoanDto>> GetAllLoans();
        Task<LoanDto> AddLoan(CreateLoanModel loanModel);
        Task UpdateLoan(int id, UpdateLoanModel loanModel);
        Task DeleteLoan(int id);
        Task ReturnLoan(int id);
    }
}
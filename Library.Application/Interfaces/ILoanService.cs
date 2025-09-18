using Library.Shared.CreationModels;
using Library.Shared.DTOs;
using Library.Shared.UpdateModels;

namespace Library.Application.Interfaces
{
    public interface ILoanService
    {
        Task<LoanDto?> GetLoanById(int id);
        Task<IEnumerable<LoanDto?>> GetAllLoans();
        Task<LoanDto?> AddLoan(CreateLoanModel loanModel);
        Task<bool> UpdateLoan(int id, UpdateLoanModel loanModel);
        Task<bool> DeleteLoan(int id);
        
        Task<bool> ReturnLoan(int id);
    }
}
using Library.Domain.Models;

namespace Library.Domain.Interfaces;

public interface ILoanRepository
{
    Task<Loan?> GetLoanById(int id);
    Task<IEnumerable<Loan>> GetAllLoans();
    Task AddLoan(Loan loan);
    Task<bool> UpdateLoan(Loan loan);
    Task<bool> DeleteLoan(int id);
    Task Save();
}
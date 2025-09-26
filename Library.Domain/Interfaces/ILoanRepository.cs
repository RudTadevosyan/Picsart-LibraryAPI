using Library.Domain.Models;

namespace Library.Domain.Interfaces;

public interface ILoanRepository : IRepository<Loan>
{
    Task<Loan?> GetLoanById(int id);
    Task<IEnumerable<Loan>> GetAllLoans();
    Task AddLoan(Loan loan);
    Task UpdateLoan(Loan loan);
    Task DeleteLoan(Loan loan);
}
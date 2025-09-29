using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class LoanRepository : BaseRepository<Loan>,ILoanRepository
{
    public LoanRepository(LibraryDbContext context) : base(context) {}
    public async Task<Loan?> GetLoanById(int id)
    {
        return await _context.Loans
            .Include(l => l.Book)
            .ThenInclude(b => b.BookDetail)
            .Include(l => l.Member)
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.LoanId == id);
    }

    public async Task<IEnumerable<Loan>> GetAllLoans()
    {
        return await _context.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddLoan(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
    }

    public Task UpdateLoan(Loan loan)
    {
        _context.Loans.Update(loan);
        return Task.CompletedTask;
    }

    public Task DeleteLoan(Loan loan)
    {
        _context.Loans.Remove(loan);
        return Task.CompletedTask;
    }
}
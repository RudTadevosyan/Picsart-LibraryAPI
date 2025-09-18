using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly LibraryDbContext _context;

    public LoanRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<Loan?> GetLoanById(int id)
    {
        return await _context.Loans
            .Include(l => l.Book)
            .ThenInclude(b => b.BookDetail)
            .Include(l => l.Member)
            .FirstOrDefaultAsync(l => l.LoanId == id);
    }

    public async Task<IEnumerable<Loan>> GetAllLoans()
    {
        return await _context.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .ToListAsync();
    }

    public async Task AddLoan(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
    }

    public Task<bool> UpdateLoan(Loan loan)
    {
        _context.Loans.Update(loan);
        return Task.FromResult(true);
    }

    public async Task<bool> DeleteLoan(int id)
    {
        var loan = await _context.Loans.FindAsync(id);
        if (loan == null) return false;

        _context.Loans.Remove(loan);
        return true;
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
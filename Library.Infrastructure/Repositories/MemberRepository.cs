using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly LibraryDbContext _context;

    public MemberRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<Member?> GetMemberById(int id)
    {
        return await _context.Members
            .Include(m => m.Loans)
            .ThenInclude(l => l.Book)
            .FirstOrDefaultAsync(m => m.MemberId == id);
    }

    public async Task<IEnumerable<Member>> GetAllMembers()
    {
        return await _context.Members
            .Include(m => m.Loans)
            .ThenInclude(l => l.Book)
            .ToListAsync();
    }

    public async Task AddMember(Member member)
    {
        await _context.Members.AddAsync(member);
    }

    public Task<bool> UpdateMember(Member member)
    {
        _context.Members.Update(member);
        return Task.FromResult(true);
    }

    public async Task<bool> DeleteMember(int id)
    {
        var member = await _context.Members.FindAsync(id);
        if (member == null) return false;

        var hasLoan = await _context.Loans.AnyAsync(l => l.MemberId == id && l.ReturnDate == null);
        if (hasLoan) return false;

        _context.Members.Remove(member);
        return true;
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories;

public class MemberRepository : BaseRepository<Member>, IMemberRepository
{
    public MemberRepository(LibraryDbContext context) : base(context) {}

    public async Task<Member?> GetMemberById(int id)
    {
        return await _context.Members
            .Include(m => m.Loans)
            .ThenInclude(l => l.Book)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MemberId == id);
    }

    public async Task<IEnumerable<Member>> GetAllMembers()
    {
        return await _context.Members
            .Include(m => m.Loans)
            .ThenInclude(l => l.Book)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddMember(Member member)
    {
        await _context.Members.AddAsync(member);
    }

    public Task UpdateMember(Member member)
    {
        _context.Members.Update(member);
        return Task.CompletedTask;
    }
    public Task DeleteMember(Member member)
    {
        _context.Members.Remove(member);
        return Task.CompletedTask;
    }
}
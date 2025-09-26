using Library.Domain.Models;

namespace Library.Domain.Interfaces;

public interface IMemberRepository : IRepository<Member>
{
    Task<Member?> GetMemberById(int id);
    Task<IEnumerable<Member>> GetAllMembers();
    Task AddMember(Member member);
    Task UpdateMember(Member member);
    Task DeleteMember(Member member);
}
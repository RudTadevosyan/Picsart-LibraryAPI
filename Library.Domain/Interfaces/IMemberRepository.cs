using Library.Domain.Models;

namespace Library.Domain.Interfaces;

public interface IMemberRepository
{
    Task<Member?> GetMemberById(int id);
    Task<IEnumerable<Member>> GetAllMembers();
    Task AddMember(Member member);
    Task<bool> UpdateMember(Member member);
    Task<bool> DeleteMember(int id);
    Task Save();
}
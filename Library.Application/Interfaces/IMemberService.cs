using Library.Shared.DTOs.Member;

namespace Library.Application.Interfaces
{
    public interface IMemberService
    {
        Task<MemberDto> GetMemberById(int id);
        Task<IEnumerable<MemberDto>> GetAllMembers();
        Task<MemberDto> AddMember(CreateMemberModel memberModel);
        Task UpdateMember(int id, UpdateMemberModel memberModel);
        Task DeleteMember(int id);
        Task<bool> HasActiveLoans(int memberId);
    }
}
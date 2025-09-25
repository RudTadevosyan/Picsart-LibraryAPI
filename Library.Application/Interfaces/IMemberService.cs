using Library.Shared.CreationModels;
using Library.Shared.DTOs;
using Library.Shared.UpdateModels;

namespace Library.Application.Interfaces
{
    public interface IMemberService
    {
        Task<MemberDto?> GetMemberById(int id);
        Task<IEnumerable<MemberDto>> GetAllMembers();
        Task<MemberDto?> AddMember(CreateMemberModel memberModel);
        Task<bool> UpdateMember(int id, UpdateMemberModel memberModel);
        Task<bool> DeleteMember(int id);

        Task<bool?> HasActiveLoans(int memberId);
    }
}
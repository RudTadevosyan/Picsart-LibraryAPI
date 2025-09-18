using Library.Application.Helpers;
using Library.Application.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Shared.CreationModels;
using Library.Shared.DTOs;
using Library.Shared.UpdateModels;

namespace Library.Application.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _repository;

    public MemberService(IMemberRepository repository)
    {
        _repository = repository;
    }

    public async Task<MemberDto?> GetMemberById(int id)
    {
        var member = await _repository.GetMemberById(id);
        return member.ToDto();
    }

    public async Task<IEnumerable<MemberDto?>> GetAllMembers()
    {
        var members = await _repository.GetAllMembers();
        return members.Select(m => m.ToDto());
    }

    public async Task<MemberDto?> AddMember(CreateMemberModel memberModel)
    {
        var member = new Member
        {
            MemberName = memberModel.MemberName,
            MemberEmail = memberModel.MemberEmail
        };

        await _repository.AddMember(member);
        await _repository.Save();
        return member.ToDto();
    }

    public async Task<bool> UpdateMember(int id, UpdateMemberModel updateModel)
    {
        var existing = await _repository.GetMemberById(id);
        if (existing == null) return false;

        if (!string.IsNullOrWhiteSpace(updateModel.MemberName))
            existing.MemberName = updateModel.MemberName;

        if (!string.IsNullOrWhiteSpace(updateModel.MemberEmail))
            existing.MemberEmail = updateModel.MemberEmail;

        await _repository.UpdateMember(existing);
        await _repository.Save();
        return true;
    }

    public async Task<bool> DeleteMember(int id)
    {
        var existing = await _repository.GetMemberById(id);
        if(existing == null) return false;

        if (existing.Loans.Any(l => l.ReturnDate == null))
            throw new InvalidOperationException("Member has active loans");

        await _repository.DeleteMember(id);
        await _repository.Save();
        return true;
    }

    public async Task<bool?> HasActiveLoans(int memberId)
    {
        var member = await _repository.GetMemberById(memberId);
        if (member == null) return null;

        return member.Loans.Any(l => l.MemberId == memberId && l.ReturnDate == null);
    }
}

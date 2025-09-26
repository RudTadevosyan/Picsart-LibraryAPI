using AutoMapper;
using Library.Application.Interfaces;
using Library.Domain.CustomExceptions;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Shared.CreationModels;
using Library.Shared.DTOs;
using Library.Shared.UpdateModels;

namespace Library.Application.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _repository;
    private readonly IMapper _mapper;
    public MemberService(IMemberRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<MemberDto> GetMemberById(int id)
    {
        var member = await _repository.GetMemberById(id) ??
                     throw new NotFoundException("Member not found");
        return _mapper.Map<MemberDto>(member);
    }

    public async Task<IEnumerable<MemberDto>> GetAllMembers()
    {
        var members = await _repository.GetAllMembers();
        return _mapper.Map<IEnumerable<MemberDto>>(members);
    }

    public async Task<MemberDto> AddMember(CreateMemberModel memberModel)
    {
        var member = _mapper.Map<Member>(memberModel);

        await _repository.AddMember(member);
        await _repository.SaveChanges();
        
        return _mapper.Map<MemberDto>(member);
    }

    public async Task UpdateMember(int id, UpdateMemberModel updateModel)
    {
        var existing = await _repository.GetMemberById(id) ??
                       throw new NotFoundException("Member not found");

        _mapper.Map(updateModel, existing);

        await _repository.UpdateMember(existing);
        await _repository.SaveChanges();
    }

    public async Task DeleteMember(int id)
    {
        var existing = await _repository.GetMemberById(id) ??
                       throw new NotFoundException("Member not found");

        if (existing.Loans.Any(l => l.ReturnDate == null))
            throw new DomainException("Member has active loans");

        await _repository.DeleteMember(existing);
        await _repository.SaveChanges();
    }

    public async Task<bool> HasActiveLoans(int memberId)
    {
        var member = await _repository.GetMemberById(memberId) ??
                     throw new NotFoundException("Member not found");

        return member.Loans.Any(l => l.MemberId == memberId && l.ReturnDate == null);
    }
}

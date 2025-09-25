using AutoMapper;
using Library.Domain.Models;
using Library.Shared.CreationModels;
using Library.Shared.DTOs;
using Library.Shared.UpdateModels;

namespace Library.Application.Helpers.MappingProfiles;

public class MemberProfile: Profile
{
    public MemberProfile()
    {
        CreateMap<Member, MemberDto>()
            .ForMember(dest => dest.LoanedBooks, opt =>
                opt.MapFrom(src => src.Loans));
        
        CreateMap<CreateMemberModel, Member>();
        
        CreateMap<UpdateMemberModel, Member>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                srcMember != null && (!(srcMember is string s) || !string.IsNullOrWhiteSpace(s))));
    }
}
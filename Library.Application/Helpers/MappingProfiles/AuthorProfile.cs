using AutoMapper;
using Library.Domain.Models;
using Library.Shared.CreationModels;
using Library.Shared.DTOs;
using Library.Shared.UpdateModels;

namespace Library.Application.Helpers.MappingProfiles;

public class AuthorProfile: Profile
{
    public AuthorProfile()
    {
        CreateMap<Author, AuthorDto>()
            .ForMember(dest => dest.Books,
                opt => 
                    opt.MapFrom(src => src.Books ?? new List<Book>()));

        CreateMap<CreateAuthorModel, Author>();
        
        CreateMap<UpdateAuthorModel, Author>()
            .ForAllMembers(opt => 
                opt.Condition((src, dest, srcMember) =>
                srcMember != null && (!(srcMember is string s) || !string.IsNullOrWhiteSpace(s))));
    }
}
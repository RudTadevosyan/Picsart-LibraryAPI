using AutoMapper;
using Library.Domain.Models;
using Library.Shared.DTOs.Genre;

namespace Library.Application.Helpers.MappingProfiles;

public class GenreProfile: Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreDto>().ReverseMap();
        CreateMap<CreateGenreModel, Genre>();
        CreateMap<UpdateGenreModel, Genre>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                srcMember != null && (!(srcMember is string s) || (!string.IsNullOrWhiteSpace((s))))));
    }    
}
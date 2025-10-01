using AutoMapper;
using Library.Domain.Models;
using Library.Shared.DTOs.BookDetail;

namespace Library.Application.Helpers.MappingProfiles;

public class BookDetailProfile: Profile
{
    public BookDetailProfile()
    {
        CreateMap<BookDetail, BookDetailDto>().ReverseMap();
        
        CreateMap<CreateBookDetailModel, BookDetail>()
            .ForMember(dest => dest.BookPages, opt =>
                opt.Condition(src => src.BookPages > 0));

        CreateMap<UpdateBookDetailModel, BookDetail>()
            .ForMember(dest => dest.BookPages, opt =>
                opt.Condition(src => src.BookPages > 0))
            .ForMember(dest => dest.BookLanguage, opt =>
                opt.Condition((src, dest, srcMember) 
                    => srcMember != null && !string.IsNullOrWhiteSpace(srcMember)))
            .ForMember(dest => dest.BookDescription, opt =>
                opt.Condition((src, dest, srcMember)
                    => srcMember != null && !string.IsNullOrWhiteSpace(srcMember)));

    }
}
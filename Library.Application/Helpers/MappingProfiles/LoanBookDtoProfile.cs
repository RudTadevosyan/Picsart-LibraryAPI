using AutoMapper;
using Library.Domain.Models;
using Library.Shared.DTOs;

namespace Library.Application.Helpers.MappingProfiles
{
    //for Member -> MemberDto.LoanedBooks
    public class LoanToBookDtoProfile : Profile
    {
        public LoanToBookDtoProfile()
        {
            CreateMap<Loan, BookDto>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Book.BookId))
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.BookTitle))
                .ForMember(dest => dest.BookPublishDate, opt => opt.MapFrom(src => src.Book.BookPublishDate))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Book.AuthorId))
                .ForMember(dest => dest.BookDetail, opt => opt.MapFrom(src => src.Book.BookDetail))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Book.Genres))
                .ForMember(dest => dest.ActiveLoanId, opt => opt.MapFrom(src => src.LoanId));
        }
    }
}
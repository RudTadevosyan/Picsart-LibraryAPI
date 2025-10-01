using AutoMapper;
using Library.Domain.Models;
using Library.Shared.DTOs.Book;

namespace Library.Application.Helpers.MappingProfiles;

public class BookProfile: Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.BookDetail, opt =>
                opt.MapFrom(src => src.BookDetail))
            .ForMember(dest => dest.Genres, opt =>
                opt.MapFrom(src => src.Genres))
            .ForMember(dest => dest.ActiveLoanId, opt => opt.MapFrom(src =>
                src.Loans.LastOrDefault(l => l.ReturnDate == null)!.LoanId));


        CreateMap<CreateBookModel, Book>()
            .ForMember(dest => dest.BookDetail, opt =>
                opt.MapFrom(src => src.BookDetail))
            .ForMember(dest => dest.Genres, opt =>
                opt.Ignore()); // we do this manually in service because we need to check all genres
        
        CreateMap<UpdateBookModel, Book>()
            .ForMember(dest => dest.BookTitle, opt =>
                opt.Condition((src, dest, srcMember) => 
                    srcMember != null && !string.IsNullOrWhiteSpace(srcMember)));

    }
}
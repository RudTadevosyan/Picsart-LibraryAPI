using AutoMapper;
using Library.Domain.Models;
using Library.Shared.CreationModels;
using Library.Shared.DTOs;
using Library.Shared.UpdateModels;

namespace Library.Application.Helpers.MappingProfiles;

public class LoanProfile: Profile
{
    public LoanProfile()
    {
        CreateMap<Loan, LoanDto>().ReverseMap();
        CreateMap<CreateLoanModel, Loan>();
        CreateMap<UpdateLoanModel, Loan>() // must be mapped to null if not
            .ForMember(dest => dest.ReturnDate, 
                opt => opt.MapFrom(src => src.ReturnDate));
    }
}
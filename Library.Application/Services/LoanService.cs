using AutoMapper;
using Library.Application.Interfaces;
using Library.Domain.CustomExceptions;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Shared.DTOs.Loan;

namespace Library.Application.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;

    public LoanService(ILoanRepository repository, IBookRepository bookRepository, IMemberRepository memberRepository, IMapper mapper)
    {
        _loanRepository = repository;
        _bookRepository = bookRepository;
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public async Task<LoanDto> GetLoanById(int id)
    {
        var loan = await _loanRepository.GetLoanById(id) ??
                   throw new NotFoundException("Loan not found");
        
        return _mapper.Map<LoanDto>(loan);
    }

    public async Task<IEnumerable<LoanDto>> GetAllLoans()
    {
        var loans = await _loanRepository.GetAllLoans();
        return _mapper.Map<IEnumerable<LoanDto>>(loans);
    }

    public async Task<LoanDto> AddLoan(CreateLoanModel loanModel)
    {
        if(!await _bookRepository.CheckId(loanModel.BookId))
                throw new NotFoundException($"Book with id {loanModel.BookId} does not exist");

        if(!await _memberRepository.CheckId(loanModel.MemberId))
                throw new NotFoundException($"Member with id {loanModel.MemberId} does not exist");

        var activeLoans = await _loanRepository.GetAllLoans();
        
        if (activeLoans.Any(l => l.BookId == loanModel.BookId && l.ReturnDate == null))
                throw new DomainException("Book is already loaned");

        var loan = _mapper.Map<Loan>(loanModel);

        await _loanRepository.AddLoan(loan);
        await _loanRepository.SaveChanges();

        return _mapper.Map<LoanDto>(loan);
    }

    public async Task UpdateLoan(int id, UpdateLoanModel loanModel)
    {
        var existing = await _loanRepository.GetLoanById(id) ?? 
                       throw new NotFoundException("Loan not found");

        if(!(await _bookRepository.CheckId(loanModel.BookId)))
                throw new NotFoundException($"Book with id {loanModel.BookId} does not exist");

        if(!(await _memberRepository.CheckId(loanModel.MemberId)))
                throw new NotFoundException($"Member with id {loanModel.MemberId} does not exist");
        
        var activeLoans = await _loanRepository.GetAllLoans();
        if (activeLoans.Any(l => l.BookId == loanModel.BookId && l.ReturnDate == null))
                throw new DomainException("Book is already loaned");
        
        _mapper.Map(loanModel, existing);

        await _loanRepository.UpdateLoan(existing);
        await _loanRepository.SaveChanges();
    }

    public async Task DeleteLoan(int id)
    {
        var existing = await _loanRepository.GetLoanById(id) ??
                       throw new NotFoundException("Loan not found");

        await _loanRepository.DeleteLoan(existing);
        await _loanRepository.SaveChanges();
    }

    public async Task ReturnLoan(int id)
    {
        var loan = await _loanRepository.GetLoanById(id) ??
                   throw new NotFoundException("Loan not found");
        
        loan.ReturnDate = DateTime.UtcNow; //for postgres (UTC)
        
        await _loanRepository.UpdateLoan(loan);
        await _loanRepository.SaveChanges();
    }
}
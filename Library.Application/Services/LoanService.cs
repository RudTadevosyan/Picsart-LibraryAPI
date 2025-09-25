using AutoMapper;
using Library.Application.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Shared.CreationModels;
using Library.Shared.DTOs;
using Library.Shared.UpdateModels;

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

    public async Task<LoanDto?> GetLoanById(int id)
    {
        var loan = await _loanRepository.GetLoanById(id);
        return _mapper.Map<LoanDto>(loan);
    }

    public async Task<IEnumerable<LoanDto>> GetAllLoans()
    {
        var loans = await _loanRepository.GetAllLoans();
        return _mapper.Map<IEnumerable<LoanDto>>(loans);
    }

    public async Task<LoanDto?> AddLoan(CreateLoanModel loanModel)
    {
        var book = await _bookRepository.GetBookById(loanModel.BookId) ??
                   throw new ArgumentException($"Book with id {loanModel.BookId} does not exist");

        var member = await _memberRepository.GetMemberById(loanModel.MemberId) ?? 
                     throw new ArgumentException($"Member with id {loanModel.MemberId} does not exist");
        
        var activeLoans = await _loanRepository.GetAllLoans();
        if (activeLoans.Any(l => l.BookId == loanModel.BookId && l.ReturnDate == null))
            throw new InvalidOperationException("Book is already loaned");
        
        var loan = _mapper.Map<Loan>(loanModel);

        await _loanRepository.AddLoan(loan);
        await _loanRepository.Save();
        
        return _mapper.Map<LoanDto?>(loan);
    }

    public async Task<bool> UpdateLoan(int id, UpdateLoanModel loanModel)
    {
        var existing = await _loanRepository.GetLoanById(id);
        if(existing == null) return false;

        var book = await _bookRepository.GetBookById(loanModel.BookId) ?? 
                   throw new ArgumentException($"Book with id {loanModel.BookId} does not exist");

        var member = await _memberRepository.GetMemberById(loanModel.MemberId) ?? 
                     throw new ArgumentException($"Member with id {loanModel.MemberId} does not exist");
        
        var activeLoans = await _loanRepository.GetAllLoans();
        if (activeLoans.Any(l => l.BookId == loanModel.BookId && l.ReturnDate == null))
            throw new InvalidOperationException("Book is already loaned");
        
        _mapper.Map(loanModel, existing);

        await _loanRepository.UpdateLoan(existing);
        await _loanRepository.Save();
        return true;
    }

    public async Task<bool> DeleteLoan(int id)
    {
        var existing = await _loanRepository.GetLoanById(id);
        if(existing == null) return false;

        await _loanRepository.DeleteLoan(id);
        await _loanRepository.Save();
        return true;
    }

    public async Task<bool> ReturnLoan(int id)
    {
        var loan = await _loanRepository.GetLoanById(id);
        if(loan == null) return false;
        
        loan.ReturnDate = DateTime.UtcNow; //for postgres (UTC)
        
        await _loanRepository.UpdateLoan(loan);
        await _loanRepository.Save();
        return true;
    }
}
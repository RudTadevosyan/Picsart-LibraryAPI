using Library.Application.Helpers;
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

    public LoanService(ILoanRepository repository, IBookRepository bookRepository, IMemberRepository memberRepository)
    {
        _loanRepository = repository;
        _bookRepository = bookRepository;
        _memberRepository = memberRepository;
    }

    public async Task<LoanDto?> GetLoanById(int id)
    {
        var loan = await _loanRepository.GetLoanById(id);
        return loan.ToDto();
    }

    public async Task<IEnumerable<LoanDto?>> GetAllLoans()
    {
        var loans = await _loanRepository.GetAllLoans();
        return loans.Select(l => l.ToDto());
    }

    public async Task<LoanDto?> AddLoan(CreateLoanModel loanModel)
    {
        var book = await _bookRepository.GetBookById(loanModel.BookId);
        if(book == null)
            throw new ArgumentException($"Book with id {loanModel.BookId} does not exist");

        var member = await _memberRepository.GetMemberById(loanModel.MemberId);
        if (member == null)
            throw new ArgumentException($"Member with id {loanModel.MemberId} does not exist");
        
        var activeLoans = await _loanRepository.GetAllLoans();
        if (activeLoans.Any(l => l.BookId == loanModel.BookId && l.ReturnDate == null))
            throw new InvalidOperationException("Book is already loaned");
        
        Loan loan = new Loan
        {
            LoanDate = loanModel.LoanDate,
            BookId = loanModel.BookId,
            MemberId = loanModel.MemberId,
        };

        await _loanRepository.AddLoan(loan);
        await _loanRepository.Save();
        return loan.ToDto();
    }

    public async Task<bool> UpdateLoan(int id, UpdateLoanModel loanModel)
    {
        var existing = await _loanRepository.GetLoanById(id);
        if(existing == null) return false;

        var book = await _bookRepository.GetBookById(loanModel.BookId);
        if(book == null)
            throw new ArgumentException($"Book with id {loanModel.BookId} does not exist");

        var member = await _memberRepository.GetMemberById(loanModel.MemberId);
        if (member == null)
            throw new ArgumentException($"Member with id {loanModel.MemberId} does not exist");
        
        var activeLoans = await _loanRepository.GetAllLoans();
        if (activeLoans.Any(l => l.BookId == loanModel.BookId && l.ReturnDate == null))
            throw new InvalidOperationException("Book is already loaned");
        
        existing.LoanDate = loanModel.LoanDate;
        existing.BookId = loanModel.BookId;
        existing.MemberId = loanModel.MemberId;
        existing.ReturnDate = loanModel.ReturnDate;

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
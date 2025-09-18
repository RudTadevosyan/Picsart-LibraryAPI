using Library.Domain.Models;
using Library.Shared.DTOs;

namespace Library.Application.Helpers;

public static class MappingHelpers
{
    public static AuthorDto? ToDto(this Author? author)
    {
        if (author == null) return null;

        return new AuthorDto
        {
            AuthorId = author.AuthorId,
            AuthorName = author.AuthorName,
            AuthorEmail = author.AuthorEmail,
            Books = author.Books?.Select(b => b.ToDto()).ToList() ?? new List<BookDto>()!
        };
    }

    public static BookDetailDto? ToDto(this BookDetail? detail)
    {
        if (detail == null) return null;

        return new BookDetailDto
        {
            BookPages = detail.BookPages,
            BookLanguage = detail.BookLanguage,
            BookDescription = detail.BookDescription
        };
    }

    public static GenreDto? ToDto(this Genre? genre)
    {
        if (genre == null) return null;

        return new GenreDto
        {
            GenreId = genre.GenreId,
            GenreName = genre.GenreName
        };
    }

    public static BookDto? ToDto(this Book? book)
    {
        if (book == null) return null;

        
        
        return new BookDto
        {
            BookId = book.BookId,
            BookTitle = book.BookTitle,
            BookPublishDate = book.BookPublishDate,
            AuthorId = book.AuthorId,
            BookDetail = book.BookDetail?.ToDto(),
            Genres = book.Genres.Select(g => g.ToDto()).ToList()!,
            LoanId = book.Loans?.LastOrDefault(l => l.ReturnDate == null)?.LoanId,
        };
    }

    public static LoanDto? ToDto(this Loan? loan)
    {
        if (loan == null) return null;

        return new LoanDto
        {
            LoanId = loan.LoanId,
            LoanDate = loan.LoanDate,
            ReturnDate = loan.ReturnDate,
            BookId = loan.BookId,
            MemberId = loan.MemberId
        };
    }

    public static MemberDto? ToDto(this Member? member)
    {
        if (member == null) return null;

        return new MemberDto
        {
            MemberId = member.MemberId,
            MemberName = member.MemberName,
            MemberEmail = member.MemberEmail,
            LoanedBooks = member.Loans
                .Select(l =>
                {
                    var bookDto = l.Book.ToDto()!;
                    bookDto.LoanId = l.LoanId;
                    return bookDto;
                })
            .ToList()
        };
    }
}

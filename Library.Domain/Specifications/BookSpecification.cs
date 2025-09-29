// Library.Domain/Specifications/BookSpecifications.cs
using System.Linq.Expressions;
using Library.Domain.Models;

namespace Library.Domain.Specifications;

public class BookByTitleSpec : Specification<Book>
{
    private readonly string? _title;
    public BookByTitleSpec(string? title) => _title = title?.ToLower();

    public override Expression<Func<Book, bool>> ToExpression()
    {
        return book => string.IsNullOrWhiteSpace(_title) ||
                       book.BookTitle.ToLower().Contains(_title);
    }
}

public class BookByAuthorSpec : Specification<Book>
{
    private readonly string? _authorName;
    public BookByAuthorSpec(string? author) => _authorName = author?.ToLower();

    public override Expression<Func<Book, bool>> ToExpression()
    {
        return book => string.IsNullOrWhiteSpace(_authorName) ||
                        book.Author.AuthorName.ToLower().Contains(_authorName);
    }
}

public class BookByGenreSpec : Specification<Book>
{
    private readonly string? _genre;
    public BookByGenreSpec(string? genre) => _genre = genre?.ToLower();

    public override Expression<Func<Book, bool>> ToExpression()
    {
        return book => string.IsNullOrWhiteSpace(_genre) ||
                       book.Genres.Any(g => g.GenreName.ToLower().Contains(_genre));
    }
}

public class BookByPublishedAfterSpec : Specification<Book>
{
    private readonly DateTime? _publishedAfter;
    public BookByPublishedAfterSpec(DateTime? publisherDate) => _publishedAfter = publisherDate;

    public override Expression<Func<Book, bool>> ToExpression()
    {
        return book => !_publishedAfter.HasValue || book.BookPublishDate >= _publishedAfter.Value;
    }
}

public class BookByAvailabilitySpec : Specification<Book>
{
    private readonly bool? _isAvailable;
    public BookByAvailabilitySpec(bool? isAvailable) => _isAvailable = isAvailable;

    public override Expression<Func<Book, bool>> ToExpression()
    {
        return book => !_isAvailable.HasValue || _isAvailable.Value
                           ? !book.Loans.Any(l => l.ReturnDate == null)
                           : book.Loans.Any(l => l.ReturnDate == null);
    }
}
using FluentValidation;
using Library.Shared.DTOs.Book;

namespace Library.Shared.Validators;

public class BookFilterDtoValidator : AbstractValidator<BookFilterDto>
{
    public BookFilterDtoValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0)
            .WithMessage("Page must be greater than 0");
        
        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("PageSize must be between 1 and 100");
        
       RuleFor(x => x.SortBy)
            .Must(BeValidSortBy)
            .WithMessage("SortBy must be one of : 'TitleAsc', 'TitleDesc, 'PublishDateAsc', 'PublishDateDesc'");
       
       RuleFor(x => x.PublishedAfter)
            .Must(BeValidDate)
            .WithMessage("PublishedAfter must be a valid date");

    }

    private bool BeValidDate(DateTime? date)
    {
        return date.HasValue && date.Value <= DateTime.UtcNow;
    }

    private bool BeValidSortBy(string? sortBy)
    {
        return string.IsNullOrWhiteSpace(sortBy) ||
               new[] { "TitleAsc", "TitleDesc", "PublishDateAsc", "PublishDateDesc" }
                   .Contains(sortBy, StringComparer.OrdinalIgnoreCase);
    }
}
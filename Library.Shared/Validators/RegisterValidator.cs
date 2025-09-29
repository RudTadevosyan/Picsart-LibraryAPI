using FluentValidation;
using Library.Shared.DTOs.Auth;

namespace Library.Shared.Validators;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.Role)
            .Must(r => r is "Admin" or "Member")
            .WithMessage("Role must be a Admin or Member");
    }
}
using FluentValidation;
using Library.Shared.DTOs.Auth;

namespace Library.Shared.Validators;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(login => login.Email).NotEmpty().EmailAddress();
        RuleFor(login => login.Password).NotEmpty().MinimumLength(8);
    }
}
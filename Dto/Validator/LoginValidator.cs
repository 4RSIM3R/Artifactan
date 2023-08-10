using Artifactan.Dto.Request;
using FluentValidation;

namespace Artifactan.Dto.Validator;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).NotNull().EmailAddress();

        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}
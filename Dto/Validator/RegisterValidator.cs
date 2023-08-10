using Artifactan.Dto.Request;
using Artifactan.Entities.Master;
using FluentValidation;

namespace Artifactan.Dto.Validator;

public class RegisterValidator : AbstractValidator<RegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Username).NotNull().NotEmpty();

        RuleFor(x => x.Email).NotNull().EmailAddress();

        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}
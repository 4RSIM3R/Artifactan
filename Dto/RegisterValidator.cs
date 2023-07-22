using Artifactan.Entities.Master;
using FluentValidation;

namespace Artifactan.Dto;

public class RegisterValidator : AbstractValidator<User>
{

    public RegisterValidator()
    {

        RuleFor(x => x.Username).NotNull().NotEmpty();

        RuleFor(x => x.Email).NotNull().EmailAddress();

        RuleFor(x => x.Password).NotNull().NotEmpty();

    }

}
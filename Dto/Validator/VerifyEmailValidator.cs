using FluentValidation;
using Artifactan.Dto.Request;

namespace Artifactan.Dto.Validator;

public class VerifyEmailValidator : AbstractValidator<VerifyEmailRequest>
{
    public VerifyEmailValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotNull();
        RuleFor(x => x.Otp).NotNull();
    }
}
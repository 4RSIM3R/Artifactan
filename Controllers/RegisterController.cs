using Artifactan.Dto;
using Artifactan.Dto.Request;
using Artifactan.Handlers;
using Artifactan.Queries.Register;
using FluentValidation;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Artifactan.Controllers;

[ApiController]
[Route("register")]
public class RegisterController : ControllerBase
{
    private readonly IValidator<RegisterRequest> _registerValidator;
    private readonly IValidator<VerifyEmailRequest> _verifyEmailValidator;
    private readonly IMediator _mediator;

    public RegisterController(
        IValidator<RegisterRequest> registerValidator, 
        IValidator<VerifyEmailRequest> verifyEmailValidator,
        IMediator mediator)
    {
        _registerValidator = registerValidator;
        _verifyEmailValidator = verifyEmailValidator;
        this._mediator = mediator;
    }


    [HttpPost]
    public async Task<ActionResult> Register([FromBody] RegisterRequest payload)
    {
        var validation = await _registerValidator.ValidateAsync(payload);

        if (!validation.IsValid)
        {
            var errors = new Dictionary<string, string>();
            validation.Errors.ForEach(x => errors.Add(x.PropertyName, x.ErrorMessage));
            return UnprocessableEntity(new BaseResponse<Dictionary<string, string>>("Validation Error", errors));
        }

        var result = await _mediator.Send(new RegisterNewUser(payload));
        
        BackgroundJob.Enqueue<SendEmailHandler>(x => x.EnqueueEmailSendingJob(new SendEmailToNewUser(result)));
        
        return Ok(result);
    }
    
    [HttpPost("verify-email")]
    public async Task<ActionResult> VerificationOtp([FromBody] VerifyEmailRequest payload)
    {
        var validation = await _verifyEmailValidator.ValidateAsync(payload);
        
        if (!validation.IsValid)
        {
            var errors = new Dictionary<string, string>();
            validation.Errors.ForEach(x => errors.Add(x.PropertyName, x.ErrorMessage));
            return UnprocessableEntity(new BaseResponse<Dictionary<string, string>>("Validation Error", errors));
        }

        var result = await _mediator.Send(new VerifyEmailQuery(payload));

        return Ok(result);
    }
}
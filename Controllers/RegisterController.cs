using System.Net;
using Artifactan.Dto;
using Artifactan.Entities.Master;
using Artifactan.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Artifactan.Controllers;

[ApiController]
[Route("register")]
public class RegisterController : ControllerBase
{

    private readonly IValidator<User> registerValidator;
    private readonly IMediator mediator;

    public RegisterController(IValidator<User> registerVaidator, IMediator mediator)
    {
        registerValidator = registerVaidator;
        this.mediator = mediator;
    }


    [HttpPost]
    public async Task<ActionResult> Register([FromBody] User payload)
    {


        var validation = registerValidator.Validate(payload);

        if (!validation.IsValid)
        {
            var errors = new Dictionary<string, string>();
            validation.Errors.ForEach(x => errors.Add(x.PropertyName, x.ErrorMessage));
            return UnprocessableEntity(new BaseResponse<Dictionary<string, string>>("Validation Error", errors));
        }

        var result = await mediator.Send(new RegisterNewUser(payload));

        return Ok(result);

    }


}
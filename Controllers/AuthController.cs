using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Artifactan.Config;
using Artifactan.Dto;
using Artifactan.Dto.Request;
using Artifactan.Entities.Master;
using Artifactan.Queries.Auth;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NaCl.Core.Internal;
using Paseto;
using Paseto.Builder;
using Paseto.Cryptography.Key;

namespace Artifactan.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly IMediator _mediator;

    public AuthController(IValidator<LoginRequest> loginValidator, IMediator mediator)
    {
        _loginValidator = loginValidator;
        _mediator = mediator;
    }

    // private readonly byte[] _symmetricKey = CryptoBytes.FromHexString("707172737475767778797a7b7c7d7e7f808182838485868788898a8b8c8d8e8f");
    //
    // [HttpPost("login")]
    // public async Task<ActionResult> Login()
    // {
    //
    //     var token = new PasetoBuilder()
    //     .UseV4(new Paseto.Purpose())
    //     .AddClaim("data", "data")
    //     .WithKey(_symmetricKey, Encryption.SymmetricKey)
    //     .Encode();
    //
    //     return await Task.FromResult(Ok(token));
    // }

    [HttpPost]
    public async Task<ActionResult> Login([FromBody] LoginRequest payload)
    {
        var validation = await _loginValidator.ValidateAsync(payload);

        if (!validation.IsValid)
        {
            var errors = new Dictionary<string, string>();
            validation.Errors.ForEach(x => errors.Add(x.PropertyName, x.ErrorMessage));
            return UnprocessableEntity(new BaseResponse<Dictionary<string, string>>("Validation Error", errors));
        }

        var result = await _mediator.Send(new LoginQuery(payload));

        return Ok(result);
    }
}
using Microsoft.AspNetCore.Mvc;
using NaCl.Core.Internal;
using Paseto;
using Paseto.Builder;
using Paseto.Cryptography.Key;

namespace Artifactan.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{

    private readonly byte[] _symmetricKey = CryptoBytes.FromHexString("707172737475767778797a7b7c7d7e7f808182838485868788898a8b8c8d8e8f");

    [HttpPost]
    public async Task<ActionResult> Login()
    {

        var token = new PasetoBuilder()
        .UseV4(new Paseto.Purpose())
        .AddClaim("data", "data")
        .WithKey(_symmetricKey, Encryption.SymmetricKey)
        .Encode();

        return await Task.FromResult(Ok(token));
    }

}
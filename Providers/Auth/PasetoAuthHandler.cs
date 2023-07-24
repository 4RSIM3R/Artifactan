using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Artifactan.Providers.Auth;

public class PasetoAuthHandler : AuthenticationHandler<PasetoAuthProvider>
{

    public PasetoAuthHandler(
        IOptionsMonitor<PasetoAuthProvider> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock
        ) : base(options, logger, encoder, clock)
    { }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Add Your Logic Here
        if (!Request.Headers.ContainsKey(Options.TokenHeaderName))
        {
            return AuthenticateResult.Fail($"Missing header: {Options.TokenHeaderName}");
        }

        var claims = new List<Claim>()
        {
            new Claim("FirstName", "Juan")
        };
        var claimsIdentity = new ClaimsIdentity(claims, this.Scheme.Name);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, this.Scheme.Name));
    }
}
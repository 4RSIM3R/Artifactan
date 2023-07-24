using Microsoft.AspNetCore.Authentication;

namespace Artifactan.Providers.Auth;

public class PasetoAuthProvider : AuthenticationSchemeOptions
{

    public const string DefaultScheme = "PasetoAuthScheme";
    public string TokenHeaderName { get; set; } = "Token";

}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Artifactan.Config;
using Artifactan.Entities.Master;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Artifactan.Utils;

public class JwtUtils
{
    private readonly JwtConfig _jwtConfig;

    public JwtUtils(IOptions<JwtConfig> jwtConfig)
    {
        _jwtConfig = jwtConfig.Value;
    }


    public string GenerateToken(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var descriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
            }),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
        };

        var token = handler.CreateToken(descriptor);

        return handler.WriteToken(token);
    }

    public string? ValidateToken(string? token)
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        try
        {
            handler.ValidateToken(token, new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == "Id").Value.ToString();
            return userId;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}
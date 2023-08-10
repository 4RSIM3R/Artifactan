using Artifactan.Dto.Response;
using Artifactan.Entities;
using Artifactan.Queries.Auth;
using Artifactan.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Artifactan.Handlers.Auth;

public class LoginHandler : IRequestHandler<LoginQuery, LoginResponse>
{

    private readonly JwtUtils _jwtUtils;
    private readonly ApplicationDbContext _dbContext;

    public LoginHandler(JwtUtils jwtUtils, ApplicationDbContext dbContext)
    {
        _jwtUtils = jwtUtils;
        _dbContext = dbContext;
    }

    public Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {

        var user =  _dbContext.Users.FirstOrDefaultAsync(user => user.Email == request.Request.Email, cancellationToken: cancellationToken);

        if (user.Result != null)
        {
            var token = _jwtUtils.GenerateToken(user.Result);
            return Task.FromResult(new LoginResponse()
            {
                Token = token,
                RefreshToken = user.Result?.Password ?? ""
            });
        }
        else
        {
            return Task.FromResult(new LoginResponse()
            {
                Token = user.Result?.Email ?? "",
                RefreshToken = user.Result?.Password ?? ""
            });
        }

        
    }
}
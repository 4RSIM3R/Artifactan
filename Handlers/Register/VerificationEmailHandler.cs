using Artifactan.Dto.Response;
using Artifactan.Entities;
using Artifactan.Queries.Register;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Artifactan.Handlers.Register;

public class VerificationEmailHandler : IRequestHandler<VerifyEmailQuery, VerifyEmailResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public VerificationEmailHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<VerifyEmailResponse> Handle(VerifyEmailQuery request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        
        var user = _dbContext.Users.FirstOrDefaultAsync(user => user.Email == request.Request.Email,
            cancellationToken: cancellationToken);

        try
        {
            if (user.Result != null)
            {
                if (user.Result.Otp == request.Request.Otp)
                {
                    user.Result.IsActive = true;
                    user.Result.Otp = null;
                    
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return await Task.FromResult(new VerifyEmailResponse()
                    {
                        Message = "success"
                    });
                }
                else
                {
                    return await Task.FromResult(new VerifyEmailResponse()
                    {
                        Message = "kode otp salah"
                    });
                }
            }
            else   {
                return await Task.FromResult(new VerifyEmailResponse()
                {
                    Message = "User tidak ditemukan"
                });  
            }
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw new BadHttpRequestException(exception.Message, exception);
        }
    }
}
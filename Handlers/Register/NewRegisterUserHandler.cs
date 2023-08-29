using Artifactan.Entities;
using Artifactan.Entities.Master;
using Artifactan.Queries.Register;
using MediatR;

namespace Artifactan.Handlers.Register;

public class NewRegisterUserHandler : IRequestHandler<RegisterNewUser, User>
{
    private readonly ApplicationDbContext _dbContext;

    public NewRegisterUserHandler(ApplicationDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<User> Handle(RegisterNewUser request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            request.User.Password = BCrypt.Net.BCrypt.HashPassword(request.User.Password);
            var user = new User()
            {
                Password = request.User.Password,
                Email = request.User.Email,
                Username = request.User.Username,
                Otp = generateOtpToken()
            };

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return user;
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw new BadHttpRequestException(exception.Message, exception);
        }
    }
    
    private string generateOtpToken()
    {
        Random rnd = new Random();
        string randomNumber = (rnd.Next(1000,9999)).ToString();

        return randomNumber;
    }
}
using Artifactan.Entities;
using Artifactan.Entities.Master;
using Artifactan.Queries;
using MediatR;

namespace Artifactan.Handlers;

public class NewRegisterUserHandler : IRequestHandler<RegisterNewUser, User>
{

    private readonly ApplicationDbContext dbContext;

    public NewRegisterUserHandler(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<User> Handle(RegisterNewUser request, CancellationToken cancellationToken)
    {

        using var transaction = dbContext.Database.BeginTransaction();

        try
        {
            request.User.Password = BCrypt.Net.BCrypt.HashPassword(request.User.Password);
            // request.User.Password = BCrypt.HashPassword("my password");
            await dbContext.Users.AddAsync(request.User, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            transaction.Commit();
            return new User
            {
                Id = request.User.Id,
                Email = request.User.Email,
                CreatedAt = request.User.CreatedAt,
                UpdatedAt = request.User.UpdatedAt,
            };
        }
        catch (Exception exception)
        {
            transaction.Rollback();
            throw new BadHttpRequestException(exception.Message, exception);
        }


    }
}
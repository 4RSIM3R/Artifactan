using Artifactan.Queries;
using Artifactan.Queries.Register;
using MediatR;

namespace Artifactan.Handlers;

public class SendEmailHandler : INotificationHandler<SendEmailToNewUser>
{
    public Task Handle(SendEmailToNewUser notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
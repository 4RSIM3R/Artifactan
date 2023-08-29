using Artifactan.Queries;
using Artifactan.Queries.Register;
using Artifactan.Utils;
using MediatR;

namespace Artifactan.Handlers;

public class SendEmailHandler : INotificationHandler<SendEmailToNewUser>
{
    private readonly SendEmailUtils _sendEmailUtils;

    public SendEmailHandler(SendEmailUtils sendEmailUtils)
    {
        _sendEmailUtils = sendEmailUtils;
    }
    public Task Handle(SendEmailToNewUser notification, CancellationToken cancellationToken)
    {
        string message =
            $@"<p>Please use the below token to verify your email address </p>
                            <p><code>{notification.User.Otp}</code></p>";
        
        _sendEmailUtils.Send(notification.User.Email, "Welcome to Our Platform", message);
        
        return Task.CompletedTask;
    }
}
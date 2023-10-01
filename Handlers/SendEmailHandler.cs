using Artifactan.Queries;
using Artifactan.Queries.Register;
using Artifactan.Utils;
using Hangfire;
using MediatR;

namespace Artifactan.Handlers;

public class SendEmailHandler : INotificationHandler<SendEmailToNewUser>
{
    private readonly SendEmailUtils _sendEmailUtils;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public SendEmailHandler(SendEmailUtils sendEmailUtils, IBackgroundJobClient backgroundJobClient)
    {
        _sendEmailUtils = sendEmailUtils;
        _backgroundJobClient = backgroundJobClient ?? throw new ArgumentNullException(nameof(backgroundJobClient));
    }
    public Task Handle(SendEmailToNewUser notification, CancellationToken cancellationToken)
    {
        EnqueueEmailSendingJob(notification);
        
        return Task.CompletedTask;
    }
    
    public void EnqueueEmailSendingJob(SendEmailToNewUser notification)
    {
        // _backgroundJobClient.Enqueue(() => Handle(notification, CancellationToken.None));
        string message =
            $@"<p>Please use the below token to verify your email address </p>
                            <p><code>{notification.User.Otp}</code></p>";
        
        _sendEmailUtils.Send(notification.User.Email, "Welcome to Our Platform", message);
    }
}
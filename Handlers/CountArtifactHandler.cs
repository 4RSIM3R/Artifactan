using Artifactan.Queries;
using MediatR;

namespace Artifactan.Handlers;

public class CountArtifactHandler : INotificationHandler<NotifyArtifactQuery>
{
    public Task Handle(NotifyArtifactQuery notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("Do your background job here");
        return Task.CompletedTask;
    }
}
using MediatR;

namespace Artifactan.Queries
{
    public record GetArtifactQuery(String Id) : IRequest<String>;
    public record NotifyArtifactQuery(String Id) : INotification;

}

using Artifactan.Queries;
using MediatR;

namespace Artifactan.Handlers;

public class GetArtifactHandler : IRequestHandler<GetArtifactQuery, String>
{
    public async Task<string> Handle(GetArtifactQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult($"Handler 1 => {request.Id}");
    }
}
using Artifactan.Dto.Request;
using Artifactan.Dto.Response;
using MediatR;

namespace Artifactan.Queries.Auth;

public record LoginQuery(LoginRequest Request) : IRequest<LoginResponse>;
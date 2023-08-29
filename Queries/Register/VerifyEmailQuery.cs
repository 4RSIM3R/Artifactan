using Artifactan.Dto;
using Artifactan.Dto.Request;
using Artifactan.Dto.Response;
using MediatR;

namespace Artifactan.Queries.Register;

public record VerifyEmailQuery(VerifyEmailRequest Request): IRequest<VerifyEmailResponse>;
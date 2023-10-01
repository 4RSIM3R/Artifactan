using Artifactan.Dto.Request;
using Artifactan.Entities.Master;
using MediatR;

namespace Artifactan.Queries.Register;

public record RegisterNewUser(RegisterRequest User) : IRequest<User>;
public record SendEmailToNewUser(User User) : INotification;
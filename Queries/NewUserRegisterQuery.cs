using Artifactan.Entities.Master;
using MediatR;

namespace Artifactan.Queries;

public record RegisterNewUser(User User) : IRequest<User>;
public record SendEmailToNewUser(User User) : INotification;
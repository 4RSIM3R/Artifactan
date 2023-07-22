using Artifactan.Entities.Master;
using FluentValidation;

namespace Artifactan.Dto;

public static class ServiceCollectionExtension
{

    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<User>, RegisterValidator>();
        return services;
    }

}
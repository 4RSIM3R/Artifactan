using Artifactan.Dto.Request;
using Artifactan.Dto.Validator;
using Artifactan.Entities.Master;
using FluentValidation;

namespace Artifactan.Dto;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RegisterRequest>, RegisterValidator>();
        services.AddScoped<IValidator<LoginRequest>, LoginValidator>();

        return services;
    }
}
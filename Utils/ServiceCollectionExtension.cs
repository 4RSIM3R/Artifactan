using Artifactan.Config;

namespace Artifactan.Utils;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddLocalUtils(this IServiceCollection services)
    {
        services.AddScoped<JwtConfig>();
        services.AddScoped<JwtUtils>();
        services.AddScoped<SendEmailConfig>();
        services.AddScoped<SendEmailUtils>();
        return services;
    }
}
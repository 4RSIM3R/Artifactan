using Artifactan.Config;

namespace Artifactan.Utils;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddLocalUtils(this IServiceCollection services)
    {
        services.AddScoped<JwtConfig>();
        services.AddScoped<JwtUtils>();
        return services;
    }
}
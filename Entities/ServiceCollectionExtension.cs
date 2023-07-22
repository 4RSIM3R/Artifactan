using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Artifactan.Entities;

public static class ServiceCollectionExtension
{

    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services)
    {

        services.AddDbContext<ApplicationDbContext>((provider, options) =>
        {
            var dbOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            var connectionString = $"Server={dbOptions.Server};Database={dbOptions.Database};Uid={dbOptions.Username};Pwd={dbOptions.Password};";
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        return services;

    }


}
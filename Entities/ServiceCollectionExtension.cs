namespace Artifactan.Entities;

public static class ServiceCollectionExtension {

    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services) {

        services.AddDbContext<ApplicationDbContext>((provider, options) => {
            
        });

        return services;

    }



    

}
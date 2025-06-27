namespace Micro.Web.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddRefitFor<TInterface> (this IServiceCollection services, string uri )
    where TInterface : class
    {
        services.AddRefitClient<TInterface>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress= new Uri(uri);
            });
        
        return services;
    }
}
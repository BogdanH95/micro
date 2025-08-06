namespace Micro.IdentityServer.Data;

public static class DatabaseExtensions
{
    public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("Database") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need to replace the default OpenIddict entities.
                options.UseOpenIddict<Guid>();
            }
        );
        
        return builder;
    }

    public static WebApplication UseMigrations(this WebApplication  app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database
            .MigrateAsync()
            .Wait();
        
        return app;
    }
}
using System.Runtime.CompilerServices;
using Micro.IdentityServer.Services.Identity;
using OpenIddict.Abstractions;

namespace Micro.IdentityServer;

public static class IdentityExtensions
{
    public static WebApplicationBuilder AddIdentityServices(this WebApplicationBuilder builder)
    {
        AddOpenIddict(builder);
        builder.Services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>();

        return builder;
    }

    public static void InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        SeedData(context);
    }

    private static void SeedData(ApplicationDbContext context)
    {
        if (context.Roles.Any())
            return;

        context.Roles.AddRange(new Role
        {
            Name = "Admin",
            NormalizedName = "ADMIN",
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            Id = Guid.NewGuid()
        }, new Role()
        {
            Name = "User",
            NormalizedName = "USER",
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            Id = Guid.NewGuid()
        });
        context.SaveChanges();
    }

    private static void AddOpenIddict(WebApplicationBuilder builder)
    {
        builder.Services.AddOpenIddict()
            .AddCore(options =>
            {
                // Configure OpenIddict to use the Entity Framework Core stores and models.
                options.UseEntityFrameworkCore()
                    .UseDbContext<ApplicationDbContext>()
                    .ReplaceDefaultEntities<Guid>();
            })
            .AddServer(options =>
            {
                // Enable the token endpoint.
                options.SetTokenEndpointUris("/connect/token");
                options.SetAuthorizationEndpointUris("/connect/authorize");
                options.SetUserInfoEndpointUris("/connect/userinfo");


                // Enable the authorization code flow.
                options.AllowAuthorizationCodeFlow()
                    .RequireProofKeyForCodeExchange();

                //Register scopes
                options.RegisterScopes(OpenIddictConstants.Scopes.OpenId,
                    OpenIddictConstants.Scopes.Profile,
                    "api");

                // Register the signing and encryption credentials.
                options.AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate();

                // Register the ASP.NET Core host and configure the ASP.NET Core options.
                options.UseAspNetCore()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableTokenEndpointPassthrough()
                    .EnableUserInfoEndpointPassthrough()
                    .EnableStatusCodePagesIntegration();
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
            });
    }
}
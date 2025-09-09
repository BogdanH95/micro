using Micro.IdentityServer.Services.Identity;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Micro.IdentityServer;

public static class IdentityExtensions
{
    public static WebApplicationBuilder AddIdentityServices(this WebApplicationBuilder builder)
    {
        builder
            .AddOpenIddict()
            .ConfigureIdentitySecurity()
            .ConfigureCookies();

        builder.Services
            .AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>();

        return builder;
    }

    private static WebApplicationBuilder ConfigureIdentitySecurity(this WebApplicationBuilder builder)
    {
        //TODO break this into configurePSWRD and configure Lockout etc
        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true; // Special char
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredUniqueChars = 4;
        });
        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        });
        builder.Services.Configure<IdentityOptions>(options =>
        {
            // User settings
            options.User.RequireUniqueEmail = true;
        });
        //password history to be implemented. 

        return builder;
    }

    private static WebApplicationBuilder ConfigureCookies(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true; // Prevent JavaScript access
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Only send over HTTPS
            options.Cookie.SameSite = SameSiteMode.Strict; // Mitigate CSRF
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

            // Login/Access Denied paths
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";

            // Sliding expiration - extends session if active
            options.SlidingExpiration = true;
        });
        return builder;
    }

    public static async Task SeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        SeedRoles(context);
        await SeedClients(app);
    }

    private static async Task SeedClients(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var manager = scope.ServiceProvider
            .GetRequiredService<IOpenIddictApplicationManager>();

        // SPA (public client, PKCE, no secret)
        await EnsureClientAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "micro",
                DisplayName = "Micro",
                RedirectUris =
                {
                    new Uri("https://localhost:5055/callback"),
                    new Uri("https://localhost:5055/signin-oidc"),
                    new Uri("https://oauth.pstmn.io/v1/callback"),
                    new Uri("https://localhost:6065/callback")
                },
                Permissions =
                {
                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.Token,
                    Permissions.ResponseTypes.Code,
                    Permissions.Scopes.Profile,
                    "openid",
                    "api"
                },
                Requirements = { Requirements.Features.ProofKeyForCodeExchange }
            },
            manager);

        // // Confidential MVC client (has secret)
        //Examples of different types of client registration
        // await EnsureClientAsync(new OpenIddictApplicationDescriptor
        // {
        //     ClientId = "mvc-client",
        //     ClientSecret = "super-secret-value", // put into protected config / secret store
        //     DisplayName = "Server-side MVC client",
        //     RedirectUris = { new Uri("https://localhost:5002/signin-oidc") },
        //     Permissions =
        //     {
        //         OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
        //         OpenIddictConstants.Permissions.Endpoints.Authorization,
        //         OpenIddictConstants.Permissions.Endpoints.Token,
        //         OpenIddictConstants.Permissions.ResponseTypes.Code,
        //         OpenIddictConstants.Permissions.Scopes.Profile,
        //         "openid",
        //         "api"
        //     }
        // }, manager);
        //
        // // Resource / machine client (client credentials example)
        // await EnsureClientAsync(new OpenIddictApplicationDescriptor
        // {
        //     ClientId = "service-client",
        //     ClientSecret = "service-secret",
        //     DisplayName = "Microservice Client",
        //     Permissions =
        //     {
        //         OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
        //         OpenIddictConstants.Permissions.Endpoints.Token,
        //         "api"
        //     }
        // }, manager);
    }

    private static async Task EnsureClientAsync(OpenIddictApplicationDescriptor desc,
        IOpenIddictApplicationManager manager)
    {
        if (desc.ClientId == null) throw new InvalidOperationException("ClientId required in client registration.");
        if (await manager.FindByClientIdAsync(desc.ClientId) == null)
            await manager.CreateAsync(desc);
    }

    private static void SeedRoles(ApplicationDbContext context)
    {
        if (context.Roles.Any())
            return;

        context.Roles.AddRange(new Role
        {
            Name = "Admin",
            NormalizedName = "ADMIN",
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            Id = Guid.NewGuid()
        }, new Role
        {
            Name = "User",
            NormalizedName = "USER",
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            Id = Guid.NewGuid()
        }, new Role
        {
            Name = "Support",
            NormalizedName = "SUPPORT",
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            Id = Guid.NewGuid()
        });
        context.SaveChanges();
    }

    private static WebApplicationBuilder AddOpenIddict(this WebApplicationBuilder builder)
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
                options
                    .SetTokenEndpointUris("/connect/token")
                    .SetAuthorizationEndpointUris("/connect/authorize")
                    .SetEndSessionEndpointUris("connect/logout")
                    .SetUserInfoEndpointUris("/connect/userinfo")
                    .SetIntrospectionEndpointUris("/connect/introspect")
                    .SetRevocationEndpointUris("/connect/revoke");

                // Enable the authorization code flow.
                options.AllowAuthorizationCodeFlow()
                    .RequireProofKeyForCodeExchange();

                //Register scopes
                options.RegisterScopes(
                    Scopes.OpenId,
                    Scopes.Profile,
                    Scopes.Email,
                    Scopes.Roles,
                    "api");

                // Register the encryption credentials. This sample uses a symmetric
                // encryption key that is shared between the server and the Api2 sample
                // (that performs local token validation instead of using introspection).
                //
                // Note: in a real world application, this encryption key should be
                // stored in a safe place (e.g in Azure KeyVault, stored as a secret).
                options.AddEncryptionKey(new SymmetricSecurityKey(
                    Convert.FromBase64String("DRjd/GnduI3Efzen9V9BvbNUfc/VKgXltV7Kbk9sMkY=")));
                
                options.SetIssuer(new Uri(builder.Configuration["IssuerUri"]!));
                // Register the signing and encryption credentials.
                options.AddDevelopmentSigningCertificate();

                // Register the ASP.NET Core host and configure the ASP.NET Core options.
                options.UseAspNetCore()
                    .EnableAuthorizationEndpointPassthrough();
                // .EnableTokenEndpointPassthrough()
                // .EnableUserInfoEndpointPassthrough()
                // .EnableStatusCodePagesIntegration();
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
            });

        return builder;
    }

    public static WebApplicationBuilder AddPolicies(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("HasPremiumAccess", policy =>
                policy.RequireClaim("AccessType", "Premium"));
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
        });

        return builder;
    }
}
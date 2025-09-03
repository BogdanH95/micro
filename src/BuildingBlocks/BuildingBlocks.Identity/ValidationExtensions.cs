using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Validation.AspNetCore;

namespace BuildingBlocks.Identity;

public static class ValidationExtensions
{
    public static IServiceCollection AddIdentityValidation(this IServiceCollection services,
        IConfiguration configuration)
    {
        var identityOptions = configuration.GetSection(IdentityOptions.Identity).Get<IdentityOptions>()
                              ?? throw new ApplicationException("Identity options not found");
        return services
            .AddOpenIddictValidation(identityOptions)
            .AddAuthenticationWithOpenIddict()
            .AddCorsWithIdentityAccess(identityOptions)
            .AddAuthorization();
    }

    public static IApplicationBuilder UseIdentityValidation(this IApplicationBuilder app)
    { 
        return app
            .UseAuthentication()
            .UseAuthorization();
    }

    private static IServiceCollection AddOpenIddictValidation(this IServiceCollection services,
        IdentityOptions identityOptions)
    {
        // Register the OpenIddict validation components.
        services.AddOpenIddict()
            .AddValidation(options =>
            {
                // Note: the validation handler uses OpenID Connect discovery
                // to retrieve the issuer signing keys used to validate tokens.
                options.SetIssuer(identityOptions.Origins);
                // options.AddAudiences("micro");

                // Register the encryption credentials. This sample uses a symmetric
                // encryption key that is shared between the server and the Api2 sample
                // (that performs local token validation instead of using introspection).
                //
                // Note: in a real world application, this encryption key should be
                // stored in a safe place (e.g in Azure KeyVault, stored as a secret).
                options.AddEncryptionKey(new SymmetricSecurityKey(
                    Convert.FromBase64String(identityOptions.Key)));

                // Register the System.Net.Http integration.
                options.UseSystemNetHttp();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();
            });

        return services;
    }

    private static IServiceCollection AddAuthenticationWithOpenIddict(
        this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
        });
        return services;
    }

    private static IServiceCollection AddCorsWithIdentityAccess(this IServiceCollection services,
        IdentityOptions identityOptions)
    {
        services.AddCors(options => options.AddDefaultPolicy(policy =>
            policy.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins(identityOptions.Origins)));

        return services;
    }
}
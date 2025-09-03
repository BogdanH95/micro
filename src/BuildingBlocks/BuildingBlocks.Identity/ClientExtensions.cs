using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;

namespace BuildingBlocks.Identity;

public static class ClientExtensions
{
    public static IServiceCollection AddClientIdentityValidation(this IServiceCollection services,
        IConfiguration configuration)
    {
        var identityOptions = configuration.GetSection(IdentityOptions.Identity).Get<IdentityOptions>()
                              ?? throw new ApplicationException("Identity options not found");
        
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies"; // local cookie auth
                options.DefaultChallengeScheme = "oidc";;
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = identityOptions.Origins;
                options.ClientId = identityOptions.ClientId;
                options.ResponseType = OpenIddictConstants.ResponseTypes.Code; // Authorization Code Flow
                options.UsePkce = true;

                options.SaveTokens = true; // stores access/refresh tokens in auth cookie
                options.GetClaimsFromUserInfoEndpoint = true;

                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("api"); // your microservices API scope
                options.CallbackPath = "/callback"; // must match registered redirect URI

            });
        
        return services;
    }
}
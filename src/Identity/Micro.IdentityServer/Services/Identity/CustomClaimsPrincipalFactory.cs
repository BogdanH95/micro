using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace Micro.IdentityServer.Services.Identity;

public class CustomClaimsPrincipalFactory(
    UserManager<User> userManager,
    RoleManager<Role> roleManager,
    IOptions<IdentityOptions> options)
    : UserClaimsPrincipalFactory<User, Role>(userManager, roleManager, options)
{
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        identity.AddClaim(new Claim("FirstName", user.FirstName));
        
        return identity;

    }
}
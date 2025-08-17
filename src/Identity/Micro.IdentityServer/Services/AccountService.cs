using System.Security.Claims;
using Micro.IdentityServer.Data.Entities.Enums;
using Micro.IdentityServer.ViewModels.Account;
using OpenIddict.Abstractions;

namespace Micro.IdentityServer.Services;

public class AccountService(
    SignInManager<User> signInManager,
    UserManager<User> userManager,
    ApplicationDbContext context)
    : IAccountService
{
    public async Task<SignInResult> PasswordSignInAsync(LoginInputModel login)
    {
        var user = await userManager.FindByEmailAsync(login.Email);
        
        if (user == null) return SignInResult.Failed;
        var principal = await signInManager.CreateUserPrincipalAsync(user);
        var identity = (ClaimsIdentity)principal.Identity!;
        identity.AddClaim(new Claim("FirstName", user.FirstName));

        //Add claim based on AccessType assigned
        if (!identity.HasClaim(nameof(User.AccessType)))
        {
            identity.AddClaim(new Claim(nameof(User.AccessType), user.AccessType.ToString()));
        }


        return await signInManager.PasswordSignInAsync(user, login.Password, false, false);
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterInputModel register,
        AccessType accessType = AccessType.Regular)
    {
        var userRole = context.Roles.SingleOrDefault(r => r.Name == "User")
                       ?? throw new Exception("User Role not found");

        var user = new User
        {
            Email = register.Email,
            UserName = register.UserName,
            FirstName = register.FirstName,
            LastName = register.LastName,
            Role = userRole,
            AccessType = accessType
        };

        return await userManager.CreateAsync(user, register.Password);
    }
}
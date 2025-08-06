using Micro.IdentityServer.ViewModels.Account;

namespace Micro.IdentityServer.Services;

public interface IAccountService
{
    Task<SignInResult> PasswordSignInAsync(LoginInputModel login);
    Task<IdentityResult> RegisterUserAsync(RegisterInputModel register);
}
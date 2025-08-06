using Micro.IdentityServer.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Micro.IdentityServer.Pages.Account;

public class Login(IAccountService accountService, ILogger<Login> logger)
    : PageModel
{
    [BindProperty] public LoginInputModel Input { get; set; } = new();

    [BindProperty(SupportsGet = true)] public string? ReturnUrl { get; set; }

    public void OnGet()
    {
        //Optional: You can prepopulate or validate ReturnUrl here
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var result = await accountService.PasswordSignInAsync(Input);

        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }

        logger.LogInformation("User logged in: {Email}", Input.Email);

        //Check if the request is part of OpenIddict flow
        if (!string.IsNullOrWhiteSpace(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            return LocalRedirect(ReturnUrl);
        return Redirect("~/");
    }
}
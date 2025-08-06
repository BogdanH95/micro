using Micro.IdentityServer.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Micro.IdentityServer.Pages.Account;

public class Register(IAccountService accountService)
    : PageModel
{
    [BindProperty] public RegisterInputModel Input { get; set; } = new();
    public string? ReturnUrl { get; set; }

    public void OnGet(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;
    }


    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return Page();

        var result = await accountService.RegisterUserAsync(Input);

        if (result.Succeeded)
            return RedirectToPage("Login", new { ReturnUrl = returnUrl });

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return Page();
    }
}
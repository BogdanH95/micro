using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Micro.Web.Pages;

public class ConfirmationModel : PageModel
{
    public string Message { get; set; } = string.Empty;
    
    public void OnGet()
    {
        Message = "Your confirmation email was sent.";
    }

    public void  OnGetOrderSubmitted()
    {
        Message = "Your order was submitted successfully.";
        
    }
}
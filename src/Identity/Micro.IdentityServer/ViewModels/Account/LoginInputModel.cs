using System.ComponentModel.DataAnnotations;

namespace Micro.IdentityServer.ViewModels.Account;

public class LoginInputModel
{
    [Required, EmailAddress] 
    public string Email { get; set; } = string.Empty;

    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
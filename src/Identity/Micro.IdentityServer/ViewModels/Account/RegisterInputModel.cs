using System.ComponentModel.DataAnnotations;

namespace Micro.IdentityServer.ViewModels.Account;

public class RegisterInputModel
{
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;

    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required, DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required] public string UserName { get; set; } = string.Empty;

    [Required] public string FirstName { get; set; } = string.Empty;

    [Required] public string LastName { get; set; } = string.Empty;
}
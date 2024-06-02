using System.ComponentModel.DataAnnotations;

namespace IdentityManager.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password, ErrorMessage = "Invalid password format.")]
    public string Password { get; set; }

    [DataType(DataType.Password, ErrorMessage = "Invalid password format.")]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}

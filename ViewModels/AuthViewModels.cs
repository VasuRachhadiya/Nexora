using System.ComponentModel.DataAnnotations;

namespace Nexora.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email address is required"), EmailAddress]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required"), DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Remember this device")]
    public bool RememberMe { get; set; } = false;

    public string? ReturnUrl { get; set; }
}

public class RegisterViewModel
{
    [Required(ErrorMessage = "Full Name is required"), StringLength(100)]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required"), EmailAddress, StringLength(150)]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required"), DataType(DataType.Password), StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the Terms and Privacy Policy.")]
    [Display(Name = "I agree to the Terms of Service & Privacy Policy")]
    public bool AcceptTerms { get; set; } = false;
}

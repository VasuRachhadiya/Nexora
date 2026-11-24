using System.ComponentModel.DataAnnotations;
using Nexora.Models;

namespace Nexora.ViewModels;

public class ContactViewModel
{
    [Required(ErrorMessage = "Please enter your full name"), StringLength(100)]
    [Display(Name = "Your Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your email address"), EmailAddress, StringLength(150)]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [StringLength(150)]
    [Display(Name = "Subject")]
    public string? Subject { get; set; }

    [Required(ErrorMessage = "Please write your project inquiry or message"), StringLength(4000)]
    [Display(Name = "Message")]
    public string Message { get; set; } = string.Empty;

    [Display(Name = "Inquiry Category")]
    public string Category { get; set; } = "Project Inquiry";

    public bool SubmittedSuccessfully { get; set; } = false;
}

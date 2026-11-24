using System.ComponentModel.DataAnnotations;

namespace Nexora.Models;

public class ContactMessage
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please provide your name"), StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please provide your email address"), EmailAddress, StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [StringLength(150)]
    public string? Subject { get; set; }

    [Required(ErrorMessage = "Please enter your message"), StringLength(4000)]
    public string Message { get; set; } = string.Empty;

    [StringLength(60)]
    public string Category { get; set; } = "Project Inquiry";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsRead { get; set; } = false;

    public bool IsArchived { get; set; } = false;

    public string? AdminNotes { get; set; }
}

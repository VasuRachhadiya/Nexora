using System.ComponentModel.DataAnnotations;

namespace Nexora.Models;

public class NewsletterSubscriber
{
    public int Id { get; set; }

    [Required, EmailAddress, StringLength(150)]
    public string Email { get; set; } = string.Empty;

    public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;
}

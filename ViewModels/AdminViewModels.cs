using Nexora.Models;

namespace Nexora.ViewModels;

public class AdminDashboardViewModel
{
    public Dictionary<string, int> Metrics { get; set; } = new();
    public List<ContactMessage> RecentMessages { get; set; } = new();
    public List<ExperienceItem> Experiences { get; set; } = new();
    public List<NewsletterSubscriber> RecentSubscribers { get; set; } = new();
    public List<ApplicationUser> Users { get; set; } = new();
}

public class AdminUserViewModel
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();
}

using Nexora.Models;

namespace Nexora.ViewModels;

public class ExploreViewModel
{
    public List<ExperienceItem> Experiences { get; set; } = new();
    public List<string> Categories { get; set; } = new();
    public string ActiveCategory { get; set; } = "All";
    public string? SearchQuery { get; set; }
}

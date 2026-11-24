using Nexora.Models;

namespace Nexora.ViewModels;

public class SearchViewModel
{
    public string Query { get; set; } = string.Empty;
    public string FilterType { get; set; } = "All";
    public List<ExperienceItem> Experiences { get; set; } = new();
    public List<ServiceOffering> Services { get; set; } = new();
    public int TotalResults => Experiences.Count + Services.Count;
}

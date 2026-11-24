using Nexora.Models;

namespace Nexora.ViewModels;

public class HomeViewModel
{
    public List<ExperienceItem> FeaturedExperiences { get; set; } = new();
    public List<ServiceOffering> Services { get; set; } = new();
    public List<Testimonial> Testimonials { get; set; } = new();
    public List<SiteStatistic> Statistics { get; set; } = new();
    public ContactMessage ContactForm { get; set; } = new();
}

using Nexora.Models;

namespace Nexora.ViewModels;

public class ServicesViewModel
{
    public List<ServiceOffering> Services { get; set; } = new();
    public List<Testimonial> Testimonials { get; set; } = new();
}

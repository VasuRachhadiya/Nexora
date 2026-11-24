using Microsoft.AspNetCore.Mvc;
using Nexora.Services;
using Nexora.ViewModels;

namespace Nexora.Controllers;

public class ServicesController : Controller
{
    private readonly IServiceOfferingService _serviceOfferingService;
    private readonly ITestimonialService _testimonialService;

    public ServicesController(
        IServiceOfferingService serviceOfferingService,
        ITestimonialService testimonialService)
    {
        _serviceOfferingService = serviceOfferingService;
        _testimonialService = testimonialService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Services & Strategic Capabilities";

        var model = new ServicesViewModel
        {
            Services = await _serviceOfferingService.GetAllAsync(),
            Testimonials = await _testimonialService.GetFeaturedAsync()
        };

        return View(model);
    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nexora.Models;
using Nexora.Services;
using Nexora.ViewModels;

namespace Nexora.Controllers;

public class HomeController : Controller
{
    private readonly IExperienceService _experienceService;
    private readonly IServiceOfferingService _serviceOfferingService;
    private readonly ITestimonialService _testimonialService;
    private readonly IStatsService _statsService;
    private readonly IContactService _contactService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        IExperienceService experienceService,
        IServiceOfferingService serviceOfferingService,
        ITestimonialService testimonialService,
        IStatsService statsService,
        IContactService contactService,
        ILogger<HomeController> logger)
    {
        _experienceService = experienceService;
        _serviceOfferingService = serviceOfferingService;
        _testimonialService = testimonialService;
        _statsService = statsService;
        _contactService = contactService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new HomeViewModel
        {
            FeaturedExperiences = await _experienceService.GetFeaturedAsync(),
            Services = await _serviceOfferingService.GetAllAsync(),
            Testimonials = await _testimonialService.GetFeaturedAsync(),
            Statistics = await _statsService.GetStatisticsAsync()
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult About()
    {
        ViewData["Title"] = "About Studio";
        return View();
    }

    [HttpGet]
    public IActionResult Contact()
    {
        ViewData["Title"] = "Contact Us";
        return View(new ContactViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(ContactViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var message = new ContactMessage
        {
            Name = model.Name,
            Email = model.Email,
            Subject = model.Subject ?? "New Project Inquiry",
            Message = model.Message,
            Category = model.Category
        };

        await _contactService.SubmitMessageAsync(message);

        model.SubmittedSuccessfully = true;
        TempData["SuccessMessage"] = "Thank you! Your dispatch has reached NEXORA Studio. Our engineering director will respond within 24 hours.";

        return View(model);
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        ViewData["Title"] = "Privacy Policy";
        return View();
    }

    [HttpGet]
    public IActionResult Terms()
    {
        ViewData["Title"] = "Terms & Conditions";
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

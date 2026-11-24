using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nexora.Models;
using Nexora.Services;
using Nexora.ViewModels;

namespace Nexora.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    private readonly IStatsService _statsService;
    private readonly IContactService _contactService;
    private readonly IExperienceService _experienceService;
    private readonly INewsletterService _newsletterService;
    private readonly UserManager<ApplicationUser> _userManager;

    public DashboardController(
        IStatsService statsService,
        IContactService contactService,
        IExperienceService experienceService,
        INewsletterService newsletterService,
        UserManager<ApplicationUser> userManager)
    {
        _statsService = statsService;
        _contactService = contactService;
        _experienceService = experienceService;
        _newsletterService = newsletterService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var metrics = await _statsService.GetDashboardMetricsAsync();
        var recentMessages = (await _contactService.GetAllMessagesAsync()).Take(5).ToList();
        var experiences = await _experienceService.GetAllAsync();
        var subscribers = (await _newsletterService.GetAllAsync()).Take(5).ToList();
        var users = _userManager.Users.Take(5).ToList();

        var model = new AdminDashboardViewModel
        {
            Metrics = metrics,
            RecentMessages = recentMessages,
            Experiences = experiences,
            RecentSubscribers = subscribers,
            Users = users
        };

        return View(model);
    }
}

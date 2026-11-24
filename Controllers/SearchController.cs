using Microsoft.AspNetCore.Mvc;
using Nexora.Services;
using Nexora.ViewModels;

namespace Nexora.Controllers;

public class SearchController : Controller
{
    private readonly IExperienceService _experienceService;
    private readonly IServiceOfferingService _serviceOfferingService;

    public SearchController(
        IExperienceService experienceService,
        IServiceOfferingService serviceOfferingService)
    {
        _experienceService = experienceService;
        _serviceOfferingService = serviceOfferingService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? q, string? type = "All")
    {
        ViewData["Title"] = "Search Digital Ecosystem";

        var query = q?.Trim() ?? string.Empty;

        var experiences = await _experienceService.SearchAsync(query);
        var allServices = await _serviceOfferingService.GetAllAsync();

        var filteredServices = string.IsNullOrWhiteSpace(query)
            ? allServices
            : allServices.Where(s =>
                s.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                s.Tagline.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                s.Description.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

        if (type == "Experiences")
        {
            filteredServices.Clear();
        }
        else if (type == "Services")
        {
            experiences.Clear();
        }

        var model = new SearchViewModel
        {
            Query = query,
            FilterType = type ?? "All",
            Experiences = experiences,
            Services = filteredServices
        };

        return View(model);
    }
}

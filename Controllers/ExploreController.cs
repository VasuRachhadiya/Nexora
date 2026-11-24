using Microsoft.AspNetCore.Mvc;
using Nexora.Services;
using Nexora.ViewModels;

namespace Nexora.Controllers;

public class ExploreController : Controller
{
    private readonly IExperienceService _experienceService;

    public ExploreController(IExperienceService experienceService)
    {
        _experienceService = experienceService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? category = null, string? q = null)
    {
        ViewData["Title"] = "Explore Experiences";

        var experiences = string.IsNullOrWhiteSpace(q)
            ? await _experienceService.GetAllAsync(category)
            : await _experienceService.SearchAsync(q);

        var categories = await _experienceService.GetCategoriesAsync();

        var model = new ExploreViewModel
        {
            Experiences = experiences,
            Categories = categories,
            ActiveCategory = category ?? "All",
            SearchQuery = q
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var item = await _experienceService.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        ViewData["Title"] = $"{item.Title} — Case Study";
        ViewBag.Related = (await _experienceService.GetAllAsync(item.Category))
            .Where(e => e.Id != item.Id)
            .Take(3)
            .ToList();

        return View(item);
    }
}

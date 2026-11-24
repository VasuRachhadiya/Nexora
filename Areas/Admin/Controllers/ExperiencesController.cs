using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexora.Models;
using Nexora.Services;

namespace Nexora.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ExperiencesController : Controller
{
    private readonly IExperienceService _experienceService;

    public ExperiencesController(IExperienceService experienceService)
    {
        _experienceService = experienceService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var items = await _experienceService.GetAllAsync();
        return View(items);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new ExperienceItem());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ExperienceItem item)
    {
        if (!ModelState.IsValid)
        {
            return View(item);
        }

        await _experienceService.CreateAsync(item);
        TempData["Success"] = "Experience artifact created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _experienceService.GetByIdAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ExperienceItem item)
    {
        if (!ModelState.IsValid)
        {
            return View(item);
        }

        var updated = await _experienceService.UpdateAsync(item);
        if (!updated) return NotFound();

        TempData["Success"] = "Experience artifact updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _experienceService.DeleteAsync(id);
        TempData["Success"] = "Experience artifact removed.";
        return RedirectToAction(nameof(Index));
    }
}

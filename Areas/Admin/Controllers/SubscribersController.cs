using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexora.Services;

namespace Nexora.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SubscribersController : Controller
{
    private readonly INewsletterService _newsletterService;

    public SubscribersController(INewsletterService newsletterService)
    {
        _newsletterService = newsletterService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var list = await _newsletterService.GetAllAsync();
        return View(list);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _newsletterService.RemoveAsync(id);
        TempData["Success"] = "Subscriber removed from dispatch.";
        return RedirectToAction(nameof(Index));
    }
}

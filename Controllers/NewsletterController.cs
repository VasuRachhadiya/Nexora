using Microsoft.AspNetCore.Mvc;
using Nexora.Services;

namespace Nexora.Controllers;

public class NewsletterController : Controller
{
    private readonly INewsletterService _newsletterService;

    public NewsletterController(INewsletterService newsletterService)
    {
        _newsletterService = newsletterService;
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe([FromForm] string email)
    {
        var result = await _newsletterService.SubscribeAsync(email);
        return Json(new { success = result.Success, message = result.Message });
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexora.Services;

namespace Nexora.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class MessagesController : Controller
{
    private readonly IContactService _contactService;

    public MessagesController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(bool? unreadOnly = null)
    {
        var messages = await _contactService.GetAllMessagesAsync(unreadOnly.HasValue && unreadOnly.Value ? false : null);
        ViewBag.UnreadOnly = unreadOnly ?? false;
        return View(messages);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var message = await _contactService.GetMessageByIdAsync(id);
        if (message == null) return NotFound();

        if (!message.IsRead)
        {
            await _contactService.MarkAsReadAsync(id);
        }

        return View(message);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        await _contactService.MarkAsReadAsync(id);
        TempData["Success"] = "Message marked as read.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _contactService.DeleteMessageAsync(id);
        TempData["Success"] = "Message deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}

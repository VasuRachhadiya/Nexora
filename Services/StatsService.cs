using Microsoft.EntityFrameworkCore;
using Nexora.Data;
using Nexora.Models;

namespace Nexora.Services;

public class StatsService : IStatsService
{
    private readonly ApplicationDbContext _context;

    public StatsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SiteStatistic>> GetStatisticsAsync()
    {
        return await _context.SiteStatistics
            .OrderBy(s => s.Order)
            .ToListAsync();
    }

    public async Task<Dictionary<string, int>> GetDashboardMetricsAsync()
    {
        return new Dictionary<string, int>
        {
            ["Experiences"] = await _context.Experiences.CountAsync(),
            ["Services"] = await _context.Services.CountAsync(),
            ["Testimonials"] = await _context.Testimonials.CountAsync(),
            ["Messages"] = await _context.ContactMessages.CountAsync(),
            ["UnreadMessages"] = await _context.ContactMessages.CountAsync(m => !m.IsRead),
            ["Subscribers"] = await _context.NewsletterSubscribers.CountAsync()
        };
    }
}

using Nexora.Models;

namespace Nexora.Services;

public interface IStatsService
{
    Task<List<SiteStatistic>> GetStatisticsAsync();
    Task<Dictionary<string, int>> GetDashboardMetricsAsync();
}

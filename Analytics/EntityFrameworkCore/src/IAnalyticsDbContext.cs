using Microsoft.EntityFrameworkCore;
using RD.Analytics;

namespace Rd.Analytics.EntityFrameworkCore;

public interface IAnalyticsDbContext
{
    DbSet<AnalyticsUserAgent> UserAgents { get; set; }
    DbSet<AnalyticsConnection> Connections { get; set; }
}
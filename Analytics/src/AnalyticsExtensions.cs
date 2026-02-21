using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RD.Analytics.Data;

namespace RD.Analytics;

public static class AnalyticsExtensions
{
    extension(IServiceCollection services)
    {
        public void AddAnalytics(Action<DbContextOptionsBuilder>? dbContextOptions = null)
        {
            services.AddDbContextFactory<AnalyticsDbContext>(dbContextOptions);
        }
    }
}

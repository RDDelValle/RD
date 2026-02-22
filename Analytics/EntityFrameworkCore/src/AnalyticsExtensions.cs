using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Rd.Analytics.EntityFrameworkCore;

public static class AnalyticsExtensions
{
    extension(IServiceCollection services)
    {
        public void AddAnalytics<TContext>(Action<DbContextOptionsBuilder>? optionsAction = null) 
            where TContext : DbContext, IAnalyticsDbContext
        {
            services.AddDbContextFactory<TContext>(optionsAction);
        }
    }
}
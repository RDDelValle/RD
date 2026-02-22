using Microsoft.Extensions.DependencyInjection;

namespace RD.Analytics.Dashboard;

public static class AnalyticsDashboardExtensions
{
    extension(IServiceCollection services)
    {
        public void AddAnalyticsDashboard(Action<AnalyticsDashboardOptions>? configureOptions = null)
        {
            AnalyticsDashboardOptions options = new();
            configureOptions?.Invoke(options);
            services.ConfigureOptions(options);
        }
    }
}
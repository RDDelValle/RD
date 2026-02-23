using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace RD.Analytics.Dashboard;

public static class AnalyticsDashboardExtensions
{
    extension(IServiceCollection services)
    {
        public void AddAnalyticsDashboard(Action<AnalyticsDashboardOptions>? configureOptions = null)
        {
            services.Configure(configureOptions ?? (_ => { }));
        }
    }
}

public static class AnalyticsDashboardAssembly
{
    public static Assembly Assembly => typeof(AnalyticsDashboardAssembly).Assembly;
}
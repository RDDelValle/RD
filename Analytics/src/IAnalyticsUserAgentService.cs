using System.Linq.Expressions;

namespace RD.Analytics;

public interface IAnalyticsUserAgentService
{
    Task<Guid> GetOrCreateUserAgentIdAsync(string userAgent, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AnalyticsUserAgent>> GetUserAgentsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AnalyticsUserAgent>> GetUserAgentsAsync(Expression<Func<AnalyticsUserAgent, bool>> where, CancellationToken cancellationToken = default);
}


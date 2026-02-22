namespace RD.Analytics;

public interface IAnalyticsUserAgentService
{
    Task<Guid> GetOrCreateUserAgentIdAsync(string userAgent);
    Task<List<AnalyticsUserAgent>> GetUserAgentsAsync<T>();
    Task<List<AnalyticsUserAgent>> GetUserAgentsAsync<T>(Func<T,bool> where);
}


namespace RD.Analytics;

public interface IAnalyticsConnectionService
{
    Task CreateConnectionAsync(AnalyticsConnection model, CancellationToken cancellationToken = default);
}
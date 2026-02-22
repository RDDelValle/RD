namespace RD.Analytics;

public class AnalyticsUserAgent
{
    public virtual Guid Id { get; init; } = default!;
    public virtual string UserAgent { get; init; } = null!;
}


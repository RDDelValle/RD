namespace RD.Analytics;

public class AnalyticsConnection
{
    public Guid Id { get; init; }
    public Guid UserAgentId { get; init; }
    public string? CreatedOnAddress { get; init; }
    public int CreatedOnPort { get; init; }
    public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
}


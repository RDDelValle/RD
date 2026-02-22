namespace RD.Analytics;

public class AnalyticsConnection
{
    public virtual Guid Id { get; init; } = default!;
    public virtual Guid UserAgentId { get; init; } = default!;
    public virtual string? CreatedOnAddress { get; init; }
    public virtual int CreatedOnPort { get; init; }
    public virtual DateTime CreatedOn { get; init; } = DateTime.UtcNow;
}


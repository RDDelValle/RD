namespace RD.Analytics.Data.Models;

public sealed class Connection
{
    public Guid Id { get; init; }
    public string? UserAgent { get; init; }
    public string? IpAddress { get; init; }
    public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
}
using Microsoft.AspNetCore.Http;

namespace RD.Analytics;

public record AnalyticsCookieOptions
{
    public string Key { get; init; } = ".app.connection";
    public string ProtectorPurpose { get; init; } = "App.Connection.v1";
    public int ExpirationDays { get; init; } = 365;
    public int RefreshDays { get; init; } = 1;
    public bool HttpOnly { get; init; } = true;
    public SameSiteMode SameSite { get; init; } = SameSiteMode.Lax;
    public CookieSecurePolicy SecurePolicy { get; init; } = CookieSecurePolicy.SameAsRequest;
}


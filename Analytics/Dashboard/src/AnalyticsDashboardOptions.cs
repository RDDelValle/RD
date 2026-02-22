namespace RD.Analytics.Dashboard;

public record AnalyticsDashboardOptions
{
    public string ProjectName { get; } = "RD.Analytics.Dashboard";
    public string ProjectVersion { get; } = "1.0";
    public bool IncludeStyles { get; init; } = true;
    public bool IncludeFooter { get; init; } = true;
    public string DashboardTitle { get; init; } = "Analytics";
    public bool RequireAuthorization { get; init; } = true;
    public string[] RequireRoles { get; init; } = [];
    public string[] RequireClaims { get; init; } = [];
    public string LoginPath { get; init; } = "/Account/Login";
    public string NotAuthorizedPath { get; init; } = "/Account/NotAuthorized";
    public string ReturnUrlQueryParameter { get; init; } = "returnUrl";
}
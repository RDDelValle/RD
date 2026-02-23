namespace RD.Analytics.Dashboard;

public record AnalyticsDashboardOptions
{
    public string ProjectName { get; set; } = "RD.Analytics.Dashboard";
    public string ProjectVersion { get; set; } = "1.0";
    public bool IncludeStyles { get; set; } = true;
    public bool IncludeFooter { get; set; } = true;
    public string DashboardTitle { get; set; } = "Analytics";
    public bool RequireAuthorization { get; set; } = true;
    public string[] RequireRoles { get; set; } = [];
    public string[] RequireClaims { get; set; } = [];
    public string LoginPath { get; set; } = "/Account/Login";
    public string NotAuthorizedPath { get; set; } = "/Account/NotAuthorized";
    public string ReturnUrlQueryParameter { get; set; } = "returnUrl";
    public string ExitAnalyticsPath { get; set; } = "/";
}
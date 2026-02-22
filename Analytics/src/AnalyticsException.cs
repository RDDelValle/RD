namespace RD.Analytics;

public class AnalyticsException(string? message = null) : Exception($"Analytics Exception: {message}");
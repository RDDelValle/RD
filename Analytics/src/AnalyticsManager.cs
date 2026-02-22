using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace RD.Analytics;

public class AnalyticsManager(
    IOptions<AnalyticsCookieOptions> options,
    IDataProtectionProvider dataProtectionProvider,
    IAnalyticsConnectionService connectionService,
    IAnalyticsUserAgentService userAgentService)
{
    private readonly IDataProtector _protector = dataProtectionProvider.CreateProtector(options.Value.ProtectorPurpose);

    private readonly CookieBuilder _cookie = new()
    {
        IsEssential = true,
        HttpOnly = options.Value.HttpOnly,
        SameSite = options.Value.SameSite,
        SecurePolicy = options.Value.SecurePolicy,
        Expiration = TimeSpan.FromDays(options.Value.ExpirationDays),
    };

    /// <summary>
    /// Gets the id for the current connection
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>ConnectionId</returns>
    /// <exception cref="AnalyticsException"></exception>
    public async Task<Guid> GetConnectionId(HttpContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            if (TryGetPayloadFromContext(context, out AnalyticsCookiePayload? payload))
            {
                RefreshContextPayload(context, payload!);
            }
            else
            {
                payload = new AnalyticsCookiePayload(
                Id: await CreateNewConnectionAsync(context, cancellationToken),
                ExpiresOn: DateTime.Now.AddDays(options.Value.ExpirationDays));
                AddPayloadToContext(context, payload);
            }
            return payload!.Id;
        }
        catch (Exception e)
        {
            throw new AnalyticsException($"Error while attempting to get analytics connection Id. {e.Message}");
        }
    }


    private async Task<Guid> CreateNewConnectionAsync(HttpContext context, CancellationToken cancellationToken)
    {
        Guid userAgentId;
        try
        {
            var userAgent = context.Request.Headers.UserAgent.ToString();
            userAgent = string.IsNullOrWhiteSpace(userAgent) ? "Undefined" : userAgent;
            userAgentId = await userAgentService.GetOrCreateUserAgentIdAsync(userAgent);
        }
        catch (Exception e)
        {
            throw new AnalyticsException($"Error while attempting to get user agent. {e.Message}");
        }
        AnalyticsConnection connection = new()
        {
            UserAgentId = userAgentId,
            CreatedOnAddress = context.Connection.RemoteIpAddress?.ToString(),
            CreatedOnPort = context.Connection.RemotePort
        };
        try
        {
            await connectionService.CreateConnectionAsync(connection);
        }
        catch (Exception e)
        {
            throw new AnalyticsException($"Error while attempting to create connection. {e.Message}");
        }
        return connection.Id;
    }

    private bool TryGetPayloadFromContext(HttpContext context, out AnalyticsCookiePayload? payload)
    {
        payload = null;
        if (!context.Request.Cookies.TryGetValue(options.Value.Key, out var protectedPayload)) return false;
        var serializedPayload = _protector.Unprotect(protectedPayload);
        payload = JsonSerializer.Deserialize<AnalyticsCookiePayload>(serializedPayload);
        return true;
    }

    private void AddPayloadToContext(HttpContext context, AnalyticsCookiePayload payload)
    {
        var serializedPayload = JsonSerializer.Serialize(payload);
        var protectedPayload = _protector.Protect(serializedPayload);
        context.Response.Cookies.Append(options.Value.Key, protectedPayload, _cookie.Build(context));
    }

    private void RefreshContextPayload(HttpContext context, AnalyticsCookiePayload payload)
    {
        if (!(payload.ExpiresOn <= DateTime.Now.AddDays(options.Value.ExpirationDays - options.Value.RefreshDays))) return;
        payload = new AnalyticsCookiePayload(
                Id: payload.Id,
                ExpiresOn: DateTime.Now.AddDays(options.Value.ExpirationDays));
        AddPayloadToContext(context, payload);
    }
}


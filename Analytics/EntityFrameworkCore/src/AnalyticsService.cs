using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RD.Analytics;

namespace Rd.Analytics.EntityFrameworkCore;

public class AnalyticsService<TContext>(IDbContextFactory<TContext> dbContextFactory) 
    : IAnalyticsService, IAnalyticsConnectionService, IAnalyticsUserAgentService
    where TContext : DbContext, IAnalyticsDbContext
{
    public async Task CreateConnectionAsync(AnalyticsConnection model, CancellationToken cancellationToken = default)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        context.Connections.Add(model);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<Guid> GetOrCreateUserAgentIdAsync(string userAgent, CancellationToken cancellationToken = default)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        var id = context.UserAgents.FirstOrDefault(e=>e.UserAgent == userAgent)?.Id ?? null;
        if(id != null && id != Guid.Empty)
            return (Guid)id;

        var agent = new AnalyticsUserAgent
        {
            UserAgent = userAgent
        };
        context.UserAgents.Add(agent);
        await context.SaveChangesAsync(cancellationToken);
        return agent.Id;
    }

    public async Task<IReadOnlyList<AnalyticsUserAgent>> GetUserAgentsAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        var list = await context.UserAgents.AsNoTracking().ToListAsync(cancellationToken);
        return list;
    }

    public async Task<IReadOnlyList<AnalyticsUserAgent>> GetUserAgentsAsync(Expression<Func<AnalyticsUserAgent, bool>> where, CancellationToken cancellationToken = default)
    {
        await using var context = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        var list = await context.UserAgents.Where(where).AsNoTracking().ToListAsync(cancellationToken);
        return list;
    }
}
using Microsoft.EntityFrameworkCore;
using RD.Analytics.Data.Models;

namespace RD.Analytics.Data;

public class AnalyticsDbContext(DbContextOptions<AnalyticsDbContext> options) : DbContext(options)
{
    public DbSet<Connection> Connections { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(AnalyticsAssembly.Assembly);
    }
}
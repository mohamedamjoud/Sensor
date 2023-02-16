using Adapter.SQLLit.Models;
using Microsoft.EntityFrameworkCore;

namespace Adapter.SQLLit.Context;

public class TemperatureContext: DbContext
{
    public TemperatureContext (DbContextOptions<TemperatureContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TemperatureRequest>().ToTable("TemperatureRequest");
    }
    public DbSet<TemperatureRequest> TemperatureRequests { get; set; }
}
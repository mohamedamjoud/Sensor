using Adapter.SQLLit.Models;
using Microsoft.EntityFrameworkCore;

namespace Adapter.SQLLit.Context;

public class TemperatureContext: DbContext
{
    public TemperatureContext (DbContextOptions<TemperatureContext> options) : base(options)
    {
    }

    public TemperatureContext()
    {
    }

    
    public virtual DbSet<SensorState> SensorState { get; set; }
}
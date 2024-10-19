using Adapter.SQLite.Models;
using Microsoft.EntityFrameworkCore;

namespace Adapter.SQLite.Context;

public class TemperatureContext: DbContext
{
    public TemperatureContext (DbContextOptions<TemperatureContext> options) : base(options)
    {
    }
    
    public virtual DbSet<Sensor> Sensors { get; set; }
}
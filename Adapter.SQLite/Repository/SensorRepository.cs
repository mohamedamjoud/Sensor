using Adapter.SQLite.Context;
using Microsoft.EntityFrameworkCore;
using Models_Sensor = Adapter.SQLite.Models.Sensor;
using Sensor = Adapter.SQLite.Models.Sensor;

namespace Adapter.SQLite.Repository;

public class SensorRepository : Repository<Models_Sensor>,ISensorRepository
{
    public SensorRepository(TemperatureContext context) : base(context)
    {
    }

    public async Task<int> Save(Core.Domain.Sensor.Sensor sensor)
    {
        return await Add(new Models_Sensor(sensor.Temperature, sensor.DateTime));
    }

    public async Task<List<(float temperature,DateTime timestamp)>> RetrieveLatestSensorRequests(int size)
    {
        var sensors = await _context.Sensors
            .OrderByDescending(s => s.DateTime)
            .Take(size)
            .AsNoTracking()
            .ToListAsync();
        
        return sensors.Select(ss => (Temperature: ss.Temperature, Timestamp: ss.DateTime)).ToList();
    }
}
using Adapter.SQLLit.Context;
using Microsoft.EntityFrameworkCore;
using Sensor = Adapter.SQLLit.Models.Sensor;

namespace Adapter.SQLLit.Repository;

public class SensorRepository : Repository<Sensor>,ISensorRepository
{
    public SensorRepository(TemperatureContext context) : base(context)
    {
    }

    public async Task<int> Save(Core.Domain.Sensor.Sensor sensor)
    {
        return await Add(new Sensor(sensor.Temperature, sensor.DateTime));
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
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
        try
        {
            return await Add(new Sensor(sensor.Temperature, sensor.DateTime));
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("An error occurred while saving the entity.", ex);
        }
    }

    public async Task<List<(float temperature,DateTime timestamp)>> RetrieveLatestSensorRequests(int size)
    {
        var sensors = await _context.Sensors
            .OrderByDescending(s => s.DateTime)
            .Take(size)
            .ToListAsync();
        
        return sensors.Select(ss => (Temperature: ss.Temperature, Timestamp: ss.DateTime)).ToList();
    }
}
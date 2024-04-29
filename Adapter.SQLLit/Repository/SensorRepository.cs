using Adapter.SQLLit.Context;
using Core.ApiPort;
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

    public async Task<List<Core.Domain.Sensor.Sensor>> RetrieveLatestSensorRequests(int size,List<ISensorInterval> sensorIntervals)
    {
        var sensors =  await _context.Sensors.OrderByDescending(s => s.DateTime).Take(size).ToListAsync();
        return sensors.Select(ss => new Core.Domain.Sensor.Sensor(ss.Temperature,ss.DateTime,sensorIntervals)).ToList();
    }
}
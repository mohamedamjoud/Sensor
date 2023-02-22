using Adapter.SQLLit.Context;
using Adapter.SQLLit.Models;
using Core.Domain.Sensor;
using Core.SpiPort;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Adapter.SQLLit.Repository;

public class SensorStateRepository : Repository<SensorState>,ISensorStateRepository
{
    public SensorStateRepository(TemperatureContext context) : base(context)
    {
    }

    public async Task<int> Save(State state)
    {
        return await Add(new SensorState(state.Value,state.DateTime));
    }

    public async Task<List<State>> GetLatestRequestsStates(int size)
    {
        var sensorStates =  await _context.SensorState.OrderBy(s => s.DateTime).Take(size).ToListAsync();
        return sensorStates.Select(ss => State.Of(ss.DateTime,ss.Value)).ToList();
    }
}
using Adapter.SQLLit.Models;
using Adapter.SQLLit.Repository;
using Core.Domain.Sensor;
using Core.SpiPort;

namespace Adapter.SQLLit;

public class UnitOfWork : ISensorStateRepositoryPort
{
    public IRepository<SensorState> Repository { get; }

    public UnitOfWork(IRepository<SensorState> repository)
    {
        Repository = repository;
    }
    public async Task<int> Save(State state)
    {
        return await Repository.Add(new SensorState(state.Value,state.DateTime));
    }
}
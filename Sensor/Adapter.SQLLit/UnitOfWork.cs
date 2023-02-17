using Adapter.SQLLit.Models;
using Adapter.SQLLit.Repository;
using Core.Domain.Sensor;

namespace Adapter.SQLLit;

public class UnitOfWork : IUnitOfWork
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
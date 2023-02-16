using Adapter.SQLLit.Models;
using Adapter.SQLLit.Repository;
using Core.SpiPort;

namespace Adapter.SQLLit;

public interface IUnitOfWork: ISensorStateRepositoryPort
{
    IRepository<SensorState> Repository { get; }
}
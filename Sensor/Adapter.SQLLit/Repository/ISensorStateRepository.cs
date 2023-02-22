using Adapter.SQLLit.Models;
using Core.Domain.Sensor;
using Core.SpiPort;

namespace Adapter.SQLLit.Repository;

public interface ISensorStateRepository : IRepository<SensorState>,ISensorStateRepositoryPort
{
}
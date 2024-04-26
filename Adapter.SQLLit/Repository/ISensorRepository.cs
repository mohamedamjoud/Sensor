using Adapter.SQLLit.Models;
using Core.Domain.Sensor;
using Core.SpiPort;
using Sensor = Adapter.SQLLit.Models.Sensor;

namespace Adapter.SQLLit.Repository;

public interface ISensorRepository : IRepository<Sensor>,ISensorRepositoryPort
{
}
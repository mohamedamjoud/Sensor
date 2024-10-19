using Adapter.SQLite.Models;
using Core.Domain.Sensor;
using Core.SpiPort;
using Models_Sensor = Adapter.SQLite.Models.Sensor;
using Sensor = Adapter.SQLite.Models.Sensor;

namespace Adapter.SQLite.Repository;

public interface ISensorRepository : IRepository<Models_Sensor>,ISensorRepositoryPort
{
}
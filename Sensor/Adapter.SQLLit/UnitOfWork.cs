using Adapter.SQLLit.Models;
using Adapter.SQLLit.Repository;
using Core.Domain.Sensor;
using Core.SpiPort;

namespace Adapter.SQLLit;

public class UnitOfWork
{
    public ISensorStateRepository SensorStateRepository { get; }

    public UnitOfWork(ISensorStateRepository sensorStateRepository)
    {
        SensorStateRepository = sensorStateRepository;
    }
}
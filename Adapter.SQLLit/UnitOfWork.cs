using Adapter.SQLLit.Models;
using Adapter.SQLLit.Repository;
using Core.Domain.Sensor;
using Core.SpiPort;

namespace Adapter.SQLLit;

public class UnitOfWork
{
    public ISensorRepository SensorRepository { get; }

    public UnitOfWork(ISensorRepository sensorRepository)
    {
        SensorRepository = sensorRepository;
    }
}
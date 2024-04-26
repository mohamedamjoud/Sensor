using Core.ApiPort;
using Core.Domain.Sensor;

namespace Core.SpiPort;

public interface ISensorRepositoryPort
{
    Task<int> Save(Sensor sensor);
    Task<List<Sensor>> RetrieveLatestSensorRequests(int size,List<ISensorInterval> sensorIntervals);
}
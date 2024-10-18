using Core.Domain.Sensor;

namespace Core.SpiPort;

public interface ISensorRepositoryPort
{
    Task<int> Save(Sensor sensor);
    Task<List<(float temperature, DateTime timestamp)>> RetrieveLatestSensorRequests(int size);
}
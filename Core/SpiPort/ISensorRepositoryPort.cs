using Core.Domain.Sensor;

namespace Core.SpiPort;

public interface ISensorRepositoryPort
{
    Task<int> Save(Sensor sensor);
    Task<List<Tuple<float, DateTime>>> RetrieveLatestSensorRequests(int size);
}
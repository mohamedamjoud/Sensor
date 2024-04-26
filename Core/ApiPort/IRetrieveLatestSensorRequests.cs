using Core.Domain.Sensor;

namespace Core.ApiPort;

public interface IRetrieveLatestSensorRequests
{
    Task<List<Sensor>> Execute(int size);
}
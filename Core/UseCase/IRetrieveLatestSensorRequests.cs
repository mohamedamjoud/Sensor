using Core.Domain.Sensor;

namespace Core.UseCase;

public interface IRetrieveLatestSensorRequests
{
    Task<List<Sensor>> Execute(int size);
}
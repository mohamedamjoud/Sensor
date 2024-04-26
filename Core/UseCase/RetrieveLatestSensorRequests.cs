using Core.ApiPort;
using Core.Domain.Sensor;
using Core.SpiPort;

namespace Core.UseCase;

public class RetrieveLatestSensorRequests : IRetrieveLatestSensorRequests
{
    private readonly ISensorRepositoryPort _sensorRepository;
    private readonly List<ISensorInterval> _sensorIntervals;
    public RetrieveLatestSensorRequests(ISensorRepositoryPort sensorRepository, List<ISensorInterval> sensorIntervals)
    {
        _sensorRepository = sensorRepository;
        _sensorIntervals = sensorIntervals;
    }
    public async Task<List<Sensor>> Execute(int size)
    {
        return await _sensorRepository.RetrieveLatestSensorRequests(size,_sensorIntervals);
    }
}
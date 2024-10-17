using Core.ApiPort;
using Core.Domain.Sensor;
using Core.SpiPort;

namespace Core.UseCase;

public class RetrieveLatestSensorRequests : IRetrieveLatestSensorRequests
{
    private readonly ISensorRepositoryPort _sensorRepository;
    private readonly ISensorIntervalProvider _sensorIntervalProvider;
    public RetrieveLatestSensorRequests(ISensorRepositoryPort sensorRepository, ISensorIntervalProvider sensorIntervalProvider)
    {
        _sensorRepository = sensorRepository;
        _sensorIntervalProvider = sensorIntervalProvider;
    }
    public async Task<List<Sensor>> Execute(int size)
    {
        try
        {
            var intervals = _sensorIntervalProvider.GetIntervals();
            var latestSensorRequest = await _sensorRepository.RetrieveLatestSensorRequests(size);

            return latestSensorRequest
                .Select(sr => new Sensor(sr.Item1, sr.Item2, intervals))
                .ToList();
        }
        catch (Exception ex)
        {
            throw new RetrieveSensorDataException("An error occurred while retrieving the latest sensor requests.",ex);
        }
       
    }
}
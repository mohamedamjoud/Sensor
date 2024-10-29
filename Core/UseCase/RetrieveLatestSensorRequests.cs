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
            var intervals = _sensorIntervalProvider.GetIntervals() 
                            ?? throw new ArgumentNullException("Sensor intervals cannot be null");
            
            var latestSensorRequest = await _sensorRepository.RetrieveLatestSensorRequests(size);

            return latestSensorRequest
                .Select(sr => new Sensor(sr.temperature, sr.timestamp, intervals))
                .ToList();
        }
        catch (System.Exception ex)
        {
            throw new RetrieveSensorDataException($"Error while retrieving sensor data for size: {size}", ex);
        }
       
    }
}
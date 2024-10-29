using System.Runtime.CompilerServices;
using Core.ApiPort;
using Core.Domain.Sensor;
using Core.Exception;
using Core.SpiPort;

[assembly: InternalsVisibleTo("Core.Tests")]
namespace Core.UseCase;

public class RetrieveSensor : IRetrieveSensor
{
    private readonly ITemperatureSensorPort _temperatureSensor;
    private readonly ISensorRepositoryPort _sensorRepository;
    private readonly ISensorIntervalProvider _sensorIntervalProvider;

    public RetrieveSensor(
        ITemperatureSensorPort temperatureSensor, 
        ISensorRepositoryPort sensorRepository,
        ISensorIntervalProvider sensorIntervalProvider)
    {
        _temperatureSensor = temperatureSensor;
        _sensorRepository = sensorRepository;
        _sensorIntervalProvider = sensorIntervalProvider;
    }
    public async Task<StateEnum> Execute()
    {
        var temperature =  _temperatureSensor.GetTemperature();
        var intervals = _sensorIntervalProvider.GetIntervals();
        
        var sensor = new Sensor(temperature, intervals);
        
        try
        {
            await _sensorRepository.Save(sensor);
        }
        catch (System.Exception e)
        {
            throw new SaveSensorDataException("temperature not saved", e);
        }
        
        return sensor.State.Value;
    }
}
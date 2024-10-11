using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Core.ApiPort;
using Core.Domain.Sensor;
using Core.SpiPort;

[assembly: InternalsVisibleTo("Core.Tests")]
namespace Core.UseCase;

public class RetrieveSensor : IRetrieveSensor
{
    private readonly ITemperatureSensorPort _temperatureSensor;
    private readonly ISensorRepositoryPort _sensorRepository;
    private readonly List<ISensorInterval> _sensorInterval;

    public RetrieveSensor(
        ITemperatureSensorPort temperatureSensor, 
        ISensorRepositoryPort sensorRepository,
        List<ISensorInterval> sensorInterval)
    {
        _temperatureSensor = temperatureSensor;
        _sensorRepository = sensorRepository;
        _sensorInterval = sensorInterval;
    }
    public async Task<string> Execute()
    {
        var temperature =  _temperatureSensor.GetTemperature();
        var sensor = new Sensor(temperature,_sensorInterval);
        
        try
        {
            await _sensorRepository.Save(sensor);
          
        }
        catch (Exception e)
        {
            throw new Exception("temperature not saved");
        }
        
        return sensor.State.Value;
    }
}
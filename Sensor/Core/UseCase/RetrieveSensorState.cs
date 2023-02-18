using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Core.ApiPort;
using Core.Domain.Sensor;
using Core.SpiPort;

[assembly: InternalsVisibleTo("Core.Tests")]
namespace Core.UseCase;

public class RetrieveSensorState : IRetrieveSensorState
{
    private readonly ICaptorPort _captor;
    private readonly ISensorStateRepositoryPort _sensorStateRepository;

    public RetrieveSensorState(ICaptorPort captor, ISensorStateRepositoryPort sensorStateRepository)
    {
        _captor = captor;
        _sensorStateRepository = sensorStateRepository;
    }
    public async Task<string> Execute()
    {
        var temperature = await _captor.GetTemperature();
        var sensor = new Sensor(temperature);
        int response;
        try
        {
            response = await _sensorStateRepository.Save(sensor.State);
            //if (response != 1)
              //  throw new Exception("temperature not saved");
        }
        catch (Exception e)
        {
            throw new Exception("temperature not saved");
        }
        
        return sensor.State.Name;
    }
}
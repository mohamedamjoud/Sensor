using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Core.ApiPort;
using Core.Domain.Sensor;
using Core.SpiPort;

[assembly: InternalsVisibleTo("Core.Tests")]
namespace Core.UseCase;

internal class RetrieveSensorState : IRetrieveSensorState
{
    private readonly ICaptorPort Captor;
    private readonly ITemperatureRepositoryPort TemperatureRepository;

    internal RetrieveSensorState(ICaptorPort captor, ITemperatureRepositoryPort temperatureRepository)
    {
        Captor = captor;
        TemperatureRepository = temperatureRepository;
    }
    public async Task<string> Execute()
    {
        var temperature = await Captor.GetTemperature();
        var sensor = new Sensor(temperature);

        try
        {
            await TemperatureRepository.Save(temperature);
        }
        catch (Exception e)
        {
            throw new Exception("temperature not saved");
        }
        
        return sensor.State;
    }
}
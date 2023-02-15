using System.Text.Json.Serialization;
using Core.ApiPort;
using Core.Domain.Sonsor;
using Core.SpiPort;

namespace Core.UseCase;

public class RetrieveSensorState : IRetrieveSensorState
{
    private readonly ICaptor Captor;
    private readonly ITemperatureRepositoryPort TemperatureRepository;

    public RetrieveSensorState(ICaptor captor, ITemperatureRepositoryPort temperatureRepository)
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
using Core.SpiPort;
using Core.UseCase;
namespace Adapter.TemperatureCaptor;

public class DummyTemperatureCaptor : ICaptorPort
{
    private const int EARTH_TEMPERATURE_MAX = 100;
    private const int EARTH_TEMPERATURE_MIN = -100;
    
    public Task<int> GetTemperature()
    {
        return Task.FromResult(new Random().Next(EARTH_TEMPERATURE_MIN,EARTH_TEMPERATURE_MAX));
    }
}
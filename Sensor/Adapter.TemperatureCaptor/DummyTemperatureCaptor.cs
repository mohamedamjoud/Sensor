using Core.SpiPort;
using Core.UseCase;
namespace Adapter.TemperatureCaptor;

public class DummyTemperatureCaptor : ICaptorPort
{
    private const sbyte EARTH_TEMPERATURE_MAX = 100;
    private const sbyte EARTH_TEMPERATURE_MIN = -100;
    
    public Task<sbyte> GetTemperature()
    {
        return Task.FromResult(Convert.ToSByte(new Random().Next(EARTH_TEMPERATURE_MIN,EARTH_TEMPERATURE_MAX)));
    }
}
using Core.SpiPort;

namespace Adapter.TemperatureCaptor;

public class DummyTemperatureSensor : ITemperatureSensorPort
{
    private const sbyte EarthTemperatureMax = 100;
    private const sbyte EarthTemperatureMin = -100;
    
    public Task<sbyte> GetTemperature()
    {
        return Task.FromResult(Convert.ToSByte(new Random().Next(EarthTemperatureMin,EarthTemperatureMax)));
    }
}
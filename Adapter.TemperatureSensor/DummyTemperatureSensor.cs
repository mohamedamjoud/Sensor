using Core.SpiPort;

namespace Adapter.TemperatureCaptor;

public class DummyTemperatureSensor : ITemperatureSensorPort
{
    private const float EarthTemperatureMax = 100.0f;
    private const float EarthTemperatureMin = -100.0f;
    private readonly Random _random = new();

    public float GetTemperature()
    {
        float randomFloat = (float)(_random.NextDouble() * (EarthTemperatureMax - EarthTemperatureMin) + EarthTemperatureMin);
    
        return randomFloat;
    }
}
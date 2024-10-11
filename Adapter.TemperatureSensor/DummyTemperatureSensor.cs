using Core.SpiPort;

namespace Adapter.TemperatureCaptor;

public class DummyTemperatureSensor : ITemperatureSensorPort
{
    private const float EarthTemperatureMax = 100.0f;
    private const float EarthTemperatureMin = -100.0f;
    
    public float GetTemperature()
    {
        Random random = new Random();
    
        float randomFloat = (float)(random.NextDouble() * (EarthTemperatureMax - EarthTemperatureMin) + EarthTemperatureMin);
    
        return randomFloat;
    }
}
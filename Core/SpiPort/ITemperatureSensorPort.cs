namespace Core.SpiPort;

public interface ITemperatureSensorPort
{
    Task<sbyte> GetTemperature();
}
namespace Core.SpiPort;

public interface ICaptorPort
{
    Task<sbyte> GetTemperature();
}
namespace Core.SpiPort;

public interface ICaptorPort
{
    Task<int> GetTemperature();
}
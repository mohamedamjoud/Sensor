namespace Core.SpiPort;

public interface ICaptor
{
    Task<int> GetTemperature();
}
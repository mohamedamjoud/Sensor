namespace Core.SpiPort;

public interface ITemperatureRepositoryPort
{
    Task Save(int temperature);
}
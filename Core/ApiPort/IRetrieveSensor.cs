using Core.SpiPort;

namespace Core.ApiPort;

public interface IRetrieveSensor
{
    Task<string> Execute();
}
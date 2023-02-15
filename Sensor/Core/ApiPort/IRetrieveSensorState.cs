using Core.SpiPort;

namespace Core.ApiPort;

public interface IRetrieveSensorState
{
    Task<string> Execute();
}
using Core.Domain.Sensor;
using Core.SpiPort;

namespace Core.ApiPort;

public interface IRetrieveSensor
{
    Task<StateEnum> Execute();
}
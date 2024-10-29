using Core.Domain.Sensor;

namespace Core.UseCase;

public interface IRetrieveSensor
{
    Task<StateEnum> Execute();
}
using Core.Domain.Sensor;

namespace Core.ApiPort;

public interface IRetrieveLatestRequestsStates
{
    Task<List<State>> Execute();
}
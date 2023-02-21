using Core.Domain.Sensor;

namespace Core.SpiPort;

public interface ISensorStateRepositoryPort
{
    Task<int> Save(State state);
    Task<List<State>> GetLatestRequestsStates(int size);
}
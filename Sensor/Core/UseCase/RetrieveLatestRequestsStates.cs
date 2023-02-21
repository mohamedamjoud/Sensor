using Core.ApiPort;
using Core.Domain.Sensor;
using Core.SpiPort;

namespace Core.UseCase;

public class RetrieveLatestRequestsStates : IRetrieveLatestRequestsStates
{
    private const int Size = 15;
    private readonly ISensorStateRepositoryPort _sensorStateRepository;
    public RetrieveLatestRequestsStates(ISensorStateRepositoryPort sensorStateRepository)
    {
        _sensorStateRepository = sensorStateRepository;
    }
    public async Task<List<State>> Execute()
    {
        return await _sensorStateRepository.GetLatestRequestsStates(Size);
    }
}
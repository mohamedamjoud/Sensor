using Core.SpiPort;
using Core.UseCase;
namespace Adapter.TemperatureCaptor;

internal class DumpTemperatureCaptor : ICaptorPort
{
    public Task<int> GetTemperature()
    {
        return Task.FromResult(new Random().Next());
    }
}
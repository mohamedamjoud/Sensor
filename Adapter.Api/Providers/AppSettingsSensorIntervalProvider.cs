using Core.Domain.Sensor;
using Core.SpiPort;

namespace Adapter.Api.Providers;

public class AppSettingsSensorIntervalProvider : ISensorIntervalProvider
{
    private readonly IConfiguration _configuration;

    public AppSettingsSensorIntervalProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public List<SensorInterval> GetIntervals()
    {
        return  _configuration
            .GetSection("SensorIntervals")
            .Get<List<SensorInterval>>();
    }
}
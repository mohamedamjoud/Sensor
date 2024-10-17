using Core.Domain.Sensor;

namespace Core.SpiPort
{
    public interface ISensorIntervalProvider
    {
        List<SensorInterval> GetIntervals();
    }
}
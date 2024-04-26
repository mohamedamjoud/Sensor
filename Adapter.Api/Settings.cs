using System.Runtime.CompilerServices;
using Core.ApiPort;

namespace Adapter.Api; 

public class SensorInterval : ISensorInterval
{
    public string State { get; set; }
    public int? Min { get; set; }
    public int? Max { get; set; }
    
    public static ISensorInterval ToSensorInterval(SensorInterval sensorInterval)
    {
        return new Core.ApiPort.SensorInterval(sensorInterval.State, sensorInterval.Min,sensorInterval.Max);
    }
}
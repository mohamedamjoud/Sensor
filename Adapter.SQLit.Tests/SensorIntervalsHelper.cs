using Core.Domain.Sensor;

namespace Adapter.SQLit.Tests;

public static class SensorIntervalsHelper
{
    public static readonly List<SensorInterval> Value = new List<SensorInterval>() 
    { 
        new SensorInterval { State = "Hot", Min = 35, Max = 100 },
        new SensorInterval { State = "Cold", Min = -100, Max = 21 },
        new SensorInterval { State = "Warm", Min = 22, Max = 34 }
    };
}
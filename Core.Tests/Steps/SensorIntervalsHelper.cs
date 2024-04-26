using Core.ApiPort;
using Core.Domain.Sensor;

namespace Core.Tests.Steps;

public static class SensorIntervalsHelper
{
    public static readonly List<ISensorInterval> Value = new 
    (
        new List<ISensorInterval>() 
        { 
            new SensorInterval (StateEnum.Hot.ToString(),35,null), 
            new SensorInterval (StateEnum.Cold.ToString(),null,22), 
            new SensorInterval (StateEnum.Warm.ToString(),22,35)
        });
}
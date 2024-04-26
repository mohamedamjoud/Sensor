namespace Core.ApiPort;

public interface ISensorInterval
{
    public string State { get; }
    public int? Min { get; }
    public int? Max { get; }
}

public class SensorInterval : ISensorInterval
{
    public SensorInterval(string state, int? min, int? max)
    {
        State = state;
        Min = min;
        Max = max;
    }

    public string State { get; }
    public int? Min { get; }
    public int? Max { get; }
    
}
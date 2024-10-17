namespace Core.Domain.Sensor;

public class SensorInterval
{
    public string State { get; set; }
    public float? Min { get; set; }
    public float? Max { get; set; }
    
    public bool IsInRange(double temperature)
    {
        bool aboveMin = !Min.HasValue || temperature >= Min.Value;
        bool belowMax = !Max.HasValue || temperature <= Max.Value;

        return aboveMin && belowMax;
    }
    
}
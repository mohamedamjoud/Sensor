using Core.Domain.Exception;

namespace Core.Domain.Sensor;

public record State
{
    public string Value
    {
        get => _value.ToString();
    }

    private StateEnum _value;

    public State(float temperature, List<SensorInterval> sensorInterval)
    {
        SetState(temperature, sensorInterval);
    }

    private void SetState(float temperature, List<SensorInterval> sensorInterval)
    {
        try
        {
            var interval = sensorInterval.FirstOrDefault(i => i.IsInRange(temperature));
        
            if (interval != null)
            {
                _value = Enum.Parse<StateEnum>(interval.State, true);
            }
        }
        catch (System.Exception e)
        {
            throw new InvalidTemperatureException($"Temperature {temperature} is not within any valid range.", e);
        }
     
        
    }
}
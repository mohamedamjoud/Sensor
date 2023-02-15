using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Sonsor;

public class Sensor
{
    public string State
    {
        get => _state.Value.ToString();
    }
    private readonly State _state;
    
    public Sensor(int temperature)
    {
        _state = new State(temperature);
    }
}
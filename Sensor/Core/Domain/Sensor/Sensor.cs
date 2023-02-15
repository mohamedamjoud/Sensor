using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Sensor;

internal class Sensor
{
    internal string State
    {
        get => _state.Value.ToString();
    }
    private readonly State _state;
    
    internal Sensor(int temperature)
    {
        _state = new State(temperature);
    }
}
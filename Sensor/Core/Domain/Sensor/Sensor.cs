using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Sensor;

public class Sensor
{
    internal  State State { get; }
    internal Sensor(sbyte temperature)
    {
        State = new State(temperature);
    }
}
using System.Linq.Expressions;

namespace Core.Domain.Sensor;

public record State 
{
    internal string Name { get => _name.ToString(); }
    public  DateTime DateTime { get; }
    public sbyte Value { get; }

    private StateEnum _name;
    public State(sbyte temperature)
    {
        switch (temperature)
        {
            case >= 40:
                _name = StateEnum.Hot;
                break;
            case < 22:
                _name =  StateEnum.Cold;
                break;;
            case var t when (t >= 22 || t < 40):
                _name =  StateEnum.Warm;
                break;
        }

        DateTime = DateTime.Now;
        Value = temperature;
    }
}
using System.Linq.Expressions;

namespace Core.Domain.Sensor;

public record State 
{
    public string Name { get => _name.ToString(); }
    public  DateTime DateTime { get; }
    public int Value { get; }

    private StateEnum _name;
    public State(int temperature)
    {
        DateTime = DateTime.Now;
        Value = temperature;
        
        SetState(temperature);
    }

    private State(DateTime dateTime, int temperature)
    {
        DateTime = dateTime;
        Value = temperature;
        
        SetState(temperature);
    }

    private void SetState(int temperature)
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
    }

    public static State Of (DateTime dateTime, int temperature)
    {
        return new State(dateTime,temperature);
    }
}
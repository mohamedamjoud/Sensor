using System.Linq.Expressions;

namespace Core.Domain.Sensor;

internal record State 
{
    internal StateEnum Value { get; }

    internal State(int temperature)
    {
        switch (temperature)
        {
            case >= 40:
                Value = StateEnum.Hot;
                break;
            case < 22:
                Value =  StateEnum.Cold;
                break;;
            case var t when (t >= 22 || t < 40):
                Value =  StateEnum.Warm;
                break;
        }    
    }
}
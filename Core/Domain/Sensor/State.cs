using System.Linq.Expressions;
using Core.ApiPort;

namespace Core.Domain.Sensor;

public record State
{
    public string Value
    {
        get => _value.ToString();
    }

    private StateEnum _value;

    public State(int temperature, List<ISensorInterval> sensorInterval)
    {
       

        SetState(temperature, sensorInterval);
    }

    private void SetState(int temperature, List<ISensorInterval> sensorInterval)
    {   
        int minHot = sensorInterval.Single(si => si.State == StateEnum.Hot.ToString()).Min!.Value;
        int maxCold = sensorInterval.Single(si => si.State == StateEnum.Cold.ToString()).Max!.Value;
        int minWarm = sensorInterval.Single(si => si.State == StateEnum.Warm.ToString()).Min!.Value;
        int maxWarm = sensorInterval.Single(si => si.State == StateEnum.Warm.ToString()).Max!.Value;
        
        if(temperature >= minHot)
            _value = StateEnum.Hot;
        else if(temperature< maxCold)
            _value = StateEnum.Cold;
        else if (temperature >= minWarm || temperature < maxWarm)
            _value = StateEnum.Warm;
        else
            new Exception("temperature not valid");
    }
}
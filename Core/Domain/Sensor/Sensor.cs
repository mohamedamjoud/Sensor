using System.ComponentModel.DataAnnotations;
using Core.ApiPort;

namespace Core.Domain.Sensor;

public class Sensor
{
    public  State State { get; }
    public sbyte Temperature { get; }
    public  DateTime DateTime { get; }


    public Sensor(sbyte temperature, List<ISensorInterval> sensorInterval)
    {
        Temperature = temperature;
        DateTime = DateTime.Now;
        State = new State(temperature,sensorInterval);
    }
    
    public Sensor(sbyte temperature, DateTime dateTime, List<ISensorInterval> sensorInterval)
    {
        Temperature = temperature;
        DateTime = dateTime;
        State = new State(temperature,sensorInterval);
    }
    
}
namespace Core.Domain.Sensor;

public class Sensor
{
    public  State State { get; }
    public float Temperature { get; }
    public  DateTime DateTime { get; }


    public Sensor(float temperature, List<SensorInterval> sensorInterval)
    {
        Temperature = temperature;
        DateTime = DateTime.Now;
        State = new State(temperature,sensorInterval);
    }
    
    public Sensor(float temperature, DateTime dateTime, List<SensorInterval> sensorInterval)
    {
        Temperature = temperature;
        DateTime = dateTime;
        State = new State(temperature,sensorInterval);
    }
    
}
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Sensor;

namespace Adapter.SQLLit.Models;

public class SensorState
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Value { get; set; }
    public DateTime DateTime { get; set; }

    public SensorState()
    {
        
    }
    public SensorState(int value,DateTime dateTime)
    {
        Value = value;
        DateTime = dateTime;
    }
}
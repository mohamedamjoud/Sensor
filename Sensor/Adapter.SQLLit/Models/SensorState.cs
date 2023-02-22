using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Sensor;

namespace Adapter.SQLLit.Models;

public class SensorState
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public sbyte Value { get; set; }
    public DateTime DateTime { get; set; }
    
    public SensorState(sbyte value,DateTime dateTime)
    {
        Id = 1;
        Value = value;
        DateTime = dateTime;
    }
}
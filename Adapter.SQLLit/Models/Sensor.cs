using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Sensor;

namespace Adapter.SQLLit.Models;

public class Sensor : IEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public float Temperature { get; set; }
    public DateTime DateTime { get; set; }

    public Sensor()
    {
        
    }
    public Sensor(float temperature,DateTime dateTime)
    {
        Temperature = temperature;
        DateTime = dateTime;
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Sensor;

namespace Adapter.SQLLit.Models;

public class Sensor
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public sbyte Temperature { get; set; }
    public DateTime DateTime { get; set; }

    public Sensor()
    {
        
    }
    public Sensor(sbyte temperature,DateTime dateTime)
    {
        Temperature = temperature;
        DateTime = dateTime;
    }
}
using System.ComponentModel.DataAnnotations.Schema;
namespace Adapter.SQLLit.Models;

public class TemperatureRequest
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Value { get; set; }
    public DateTime DateTime { get; set; }
}
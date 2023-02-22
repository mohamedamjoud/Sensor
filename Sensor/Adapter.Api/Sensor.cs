using System.Text.Json.Serialization;

namespace Adapter.Api;

public record Sensor
{
    public string State { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? Date { get; set; }
}
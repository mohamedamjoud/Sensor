using System.Text.Json.Serialization;

namespace Adapter.Api;

public record SensorResponse
{
    public SensorResponse(string state, DateTime? date)
    {
        State = state;
        Date = date;
    }

    public SensorResponse(string state)
    {
        State = state;
    }

    public SensorResponse()
    {
        
    }

    public string State { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? Date { get; set; }
}
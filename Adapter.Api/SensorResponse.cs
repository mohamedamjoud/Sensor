using System.Text.Json.Serialization;

namespace Adapter.Api;

public class SensorResponse
{
    public SensorResponse(string state, DateTime? date = null)
    {
        State = state;
        Date = date;
    }

    public string State { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? Date { get; }
}
using System.Text.Json.Serialization;

namespace Adapter.Api.Dto;

public class SensorResponseDto
{
    public SensorResponseDto(string state, DateTime? date = null)
    {
        State = state;
        Date = date;
    }

    public string State { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? Date { get; }
}
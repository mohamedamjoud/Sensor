using System.Text.Json.Serialization;
using Core.Domain.Sensor;

namespace Adapter.Api.Dto;

public class SensorResponseDto
{
    public SensorResponseDto(StateEnum state, DateTime? date = null)
    {
        State = state;
        Date = date;
    }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StateEnum State { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? Date { get; }
}
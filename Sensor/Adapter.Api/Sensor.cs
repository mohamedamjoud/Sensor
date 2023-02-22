using System.Security.Principal;
using Newtonsoft.Json;

namespace Adapter.Api;

public record Sensor
{
    public string State { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public DateTime Date { get; set; }
}
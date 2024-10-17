using Adapter.Api.Dto;
using Newtonsoft.Json;
using NUnit.Framework;
using TechTalk.SpecFlow.Assist;

namespace Sensor.App.Tests.Steps;

[Binding]
public class RetrieveLatestSensorStateRequests
{
    private HttpResponseMessage Response { get; set; } = null!;
    private SensorStateContext _context;
    private List<SensorResponseDto> _sensorRequestes;
    private List<SensorResponseDto> _retrivedSensorRequestes;
    
    public RetrieveLatestSensorStateRequests(SensorStateContext context)
    {
        _context = context;
        _sensorRequestes = new List<SensorResponseDto>();
    }
    
    [Given(@"The sensor returns these temperatures")]
    public async Task GivenTheSensorReturnsTheseTemperatures(Table table)
    {
        var temperatures = table.Rows.Select(row => sbyte.Parse(row.Values.ToList()[0]));
        foreach (var temperature in temperatures)
        {
            _context.MockCaptorTemperature(temperature);
            
            var response = await _context._client.GetAsync("/api/Sensor");
            
            var content  = await response.Content.ReadAsStringAsync();
            SensorResponseDto sensor = JsonConvert.DeserializeObject<SensorResponseDto>(content); 
            
            _sensorRequestes.Add(sensor);
        }
    }

    [When(@"We try to retrieve the latest Fifteen Sensor States Requests")]
    public async Task WhenWeTryToRetriveTheLatestFifteenSensorStatesRequests()
    {
        var response = await _context._client.GetAsync("/api/Sensor/Search");
        var content  = await response.Content.ReadAsStringAsync();
        _retrivedSensorRequestes = JsonConvert.DeserializeObject<List<SensorResponseDto>>(content)
            .Select(s => new SensorResponseDto(s.State,s.Date)).ToList();
    }

    [Then(@"We got the same sensor states")]
    public void ThenWeGotTheseSensorStates()
    {
        _sensorRequestes.Reverse();
        Assert.AreEqual(_sensorRequestes.Count, _retrivedSensorRequestes.Count);
    }
}
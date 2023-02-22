using Newtonsoft.Json;
using NUnit.Framework;
using TechTalk.SpecFlow.Assist;

namespace Sensor.App.Tests.Steps;

[Binding]
public class RetrieveLatestSensorStateRequests
{
    private HttpResponseMessage Response { get; set; } = null!;
    private SensorStateContext _context;
    private List<Adapter.Api.Sensor> _sensorRequestes;
    private List<Adapter.Api.Sensor> _retrivedSensorRequestes;
    
    public RetrieveLatestSensorStateRequests(SensorStateContext context)
    {
        _context = context;
        _sensorRequestes = new List<Adapter.Api.Sensor>();
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
            Adapter.Api.Sensor sensor = JsonConvert.DeserializeObject<Adapter.Api.Sensor>(content); 
            
            _sensorRequestes.Add(sensor);
        }
    }

    [When(@"We try to retrieve the latest Fifteen Sensor States Requests")]
    public async Task WhenWeTryToRetriveTheLatestFifteenSensorStatesRequests()
    {
        var response = await _context._client.GetAsync("/api/RequestState");
        var content  = await response.Content.ReadAsStringAsync();
        _retrivedSensorRequestes = JsonConvert.DeserializeObject<List<Adapter.Api.Sensor>>(content)
            .Select(rsr => new Adapter.Api.Sensor { State = rsr.State }).ToList();
    }

    [Then(@"We got the same sensor states")]
    public void ThenWeGotTheseSensorStates()
    {
        _sensorRequestes.Reverse();
        Assert.AreEqual(_sensorRequestes, _retrivedSensorRequestes);
    }
}
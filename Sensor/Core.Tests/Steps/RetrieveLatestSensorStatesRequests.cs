using Core.ApiPort;
using Core.Domain.Sensor;
using Core.SpiPort;
using Core.UseCase;
using Moq;
using NUnit.Framework;

namespace Core.Tests.Steps;

[Binding]
public class RetrieveLatestSensorStatesRequests
{

    private SensorStateContext _context;
    private List<string> _insertedRequest;
    private List<string> _retrievedRequest;
    
    public RetrieveLatestSensorStatesRequests(SensorStateContext context)
    {
        _context = context;
        _insertedRequest = new List<string>();
        _retrievedRequest = new List<string>();
    }
    
    [Given(@"The sensor returns these temperatures")]
    public async Task GivenTheSensorReturnsTheseTemperatures(Table table)
    {
        var temperatures = table.Rows.Select(row => sbyte.Parse(row.Values.ToList()[0]));
        foreach (var temperature in temperatures)
        {
            _context.MockServices(temperature);
            var state = await _context.RetrieveSensorState.Execute();
            _insertedRequest.Add(state);
        }
    }

    [When(@"We try to retrieve the latest Fifteen Sensor States Requests\.")]
    public async Task WhenWeTryToRetrieveTheLatestFifteenSensorStatesRequests()
    {
        _retrievedRequest = await new RetriveLatestRequestesStates().Execute();
    }

    [Then(@"We got the same sensor states")]
    public void ThenWeGotTheSameSensorStates()
    {
        Assert.AreEqual(_retrievedRequest, _insertedRequest);
    }
}
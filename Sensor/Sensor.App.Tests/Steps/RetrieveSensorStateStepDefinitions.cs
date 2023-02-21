using System.Net;
using Adapter.SQLLit.Context;
using Core.SpiPort;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Sensor.App.Tests.Hooks;

namespace Sensor.App.Tests.Steps;

[Binding]
public sealed class RetrieveSensorStateStepDefinitions : CustomWebApplicationFactory<Program>
{
    private HttpResponseMessage Response { get; set; } = null!;
    private SensorStateContext _context;
    public RetrieveSensorStateStepDefinitions(SensorStateContext context)
    {
        _context = context;
    }

    [Given(@"I am a client")]
    public void GivenIAmAClient()
    {
        _context._client = _context._factory.CreateDefaultClient();
    }
    
    [When(@"Get request to controller was made")]
    public async Task WhenGetRequestToControllerWasMade()
    {
        Response = await _context._client.GetAsync("/api/Sensor");
    }
    
    [Then(@"the request status code will be 200")]
    public void ThenTheRequestStatusCodeWillBe()
    {
        Assert.AreEqual(HttpStatusCode.OK, Response.StatusCode);
    }

    [Given(@"temperature value is equal to '(.*)'")]
    public void GivenTemperatureValueIsEqualTo(sbyte temperature)
    {
        _context.MockCaptorTemperature(temperature);
    }
    
    [Then(@"the state value should be '(.*)'")]
    public async Task ThenTheStateValueShouldBe(string expectedState)
    {
        var content  = await Response.Content.ReadAsStringAsync();
        Adapter.Api.Sensor sensor = JsonConvert.DeserializeObject<Adapter.Api.Sensor>(content); 
        Assert.AreEqual(expectedState, sensor.State);
    }
}
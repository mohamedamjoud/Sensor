using System.Net;
using Adapter.Api.Dto;
using Core.Domain.Sensor;
using Newtonsoft.Json;
using NUnit.Framework;
using Sensor.App.Tests.Hooks;

namespace Sensor.App.Tests.Steps;

[Binding]
public sealed class RetrieveSensorStateSteps : CustomWebApplicationFactory<Program>
{
    private HttpResponseMessage Response { get; set; } = null!;
    private SensorStateContext _context;
    public RetrieveSensorStateSteps(SensorStateContext context)
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
        Response.EnsureSuccessStatusCode();
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
    public async Task ThenTheStateValueShouldBe(StateEnum expectedState)
    {
        var content  = await Response.Content.ReadAsStringAsync();
        SensorResponseDto sensor = JsonConvert.DeserializeObject<SensorResponseDto>(content); 
        Assert.AreEqual(expectedState, sensor.State);
    }
}
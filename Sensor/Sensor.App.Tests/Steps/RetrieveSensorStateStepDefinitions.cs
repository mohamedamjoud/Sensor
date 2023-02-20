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
    private readonly CustomWebApplicationFactory<Program> _factory;
    private  HttpClient _client;
    private readonly IServiceScope _scope;
    private readonly TemperatureContext _context;
    private HttpResponseMessage Response { get; set; } = null!;
    
    public RetrieveSensorStateStepDefinitions(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _scope = (_factory.Services.GetRequiredService<IServiceScopeFactory>()).CreateScope();
        _context = _scope.ServiceProvider.GetRequiredService<TemperatureContext>();
        _context.Database.EnsureCreated();
    }

    [Given(@"I am a client")]
    public void GivenIAmAClient()
    {
       

        _client = _factory.CreateDefaultClient();
    }
    
    [When(@"Get request to controller was made")]
    public async Task WhenGetRequestToControllerWasMade()
    {
        Response = await _client.GetAsync("/api/Sensor");
    }
    
    [Then(@"the request status code will be 200")]
    public void ThenTheRequestStatusCodeWillBe()
    {
        Assert.AreEqual(HttpStatusCode.OK, Response.StatusCode);
    }

    [Given(@"temperature value is equal to '(.*)'")]
    public void GivenTemperatureValueIsEqualTo(sbyte temperature)
    {
        MockCaptorTemperature(temperature);
    }

    private void MockCaptorTemperature(sbyte temperature)
    { 
        var captorMock = new Mock<ICaptorPort>();
           captorMock.Setup(
           c => c.GetTemperature()).Returns(Task.FromResult(temperature));
           
       _client = _factory.WithWebHostBuilder(builder =>
       {
           builder.ConfigureTestServices(services =>
           {
               services.AddSingleton(captorMock.Object);
           });

       }).CreateClient();
    }

    [Then(@"the state value should be '(.*)'")]
    public async Task ThenTheStateValueShouldBe(string expectedState)
    {
        var response  = await Response.Content.ReadAsStringAsync();
        Adapter.Api.Sensor sensor = JsonConvert.DeserializeObject<Adapter.Api.Sensor>(response); 
        Assert.AreEqual(expectedState, sensor.State);
    }
}
using System.Net;
using Adapter.SQLite.Context;
using Core.SpiPort;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Sensor.App.Tests.Hooks;
namespace Sensor.App.Tests.Steps;

public class SensorStateContext : CustomWebApplicationFactory<Program>, IDisposable
{
    
    public readonly CustomWebApplicationFactory<Program> _factory;
    public  HttpClient _client;
    private readonly IServiceScope _scope;
    private readonly TemperatureContext _context;
    
    public SensorStateContext(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _scope = (_factory.Services.GetRequiredService<IServiceScopeFactory>()).CreateScope();
        _context = _scope.ServiceProvider.GetRequiredService<TemperatureContext>();
        _context.Database.EnsureCreated();
    }
    
    public void MockCaptorTemperature(sbyte temperature)
    { 
        var captorMock = new Mock<ITemperatureSensorPort>();
        captorMock.Setup(c => c.GetTemperature()).Returns(temperature);
           
        _client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(captorMock.Object);
            });

        }).CreateClient();
    }
    
    public void DisposeClient()
    {
        _client?.Dispose();
    }
    public void Dispose()
    {
        DisposeClient();
        _scope?.Dispose();
    }
}
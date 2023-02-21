using Core.SpiPort;
using Core.ApiPort;
using Core.Domain.Sensor;
using Core.UseCase;
using Moq;
using NUnit.Framework;

namespace Core.Tests.Steps;

[Binding]
public sealed class RetrieveSensorStateDefinitions
{
    private readonly Mock<ICaptorPort> _captor;
    private IRetrieveSensorState _retrieveSensorState;
    private readonly Mock<ISensorStateRepositoryPort> _sensorStateRepository; 
    private string _state;
    private int _temperature;


    private SensorStateContext _context;
    
    public RetrieveSensorStateDefinitions(SensorStateContext context)
    {
        _context = context;
    }

    [Given(@"temperature value is equal to '(.*)'")]
    public void GivenTemperatureValueIsEqualTo(sbyte temperature)
    {
        _context.MockServices(temperature);
    }

    [When(@"we try to retrieve the sensor state")]
    public async void WhenWeTryToRetrieveTheSensorState()
    {
        _state = await _context.RetrieveSensorState.Execute();
    }

    [Then(@"the state value should be '(.*)'")]
    public void ThenTheStateValueShouldBe(string state)
    {
        Assert.AreEqual(state, _state);
    }

    [Then(@"the state will be saved")]
    public void ThenTheStateWillBeSaved()
    {
       _context.SensorStateRepository.Verify(tr=> tr.Save(It.IsAny<State>()), Times.Once());
    }
}
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
    
    public RetrieveSensorStateDefinitions()
    {
        _captor = new Mock<ICaptorPort>();
        _sensorStateRepository = new Mock<ISensorStateRepositoryPort>();
    }

    [Given(@"temperature value is equal to '(.*)'")]
    public void GivenTemperatureValueIsEqualTo(sbyte temperature)
    {
        _temperature = temperature;
        _captor.Setup(c => c.GetTemperature()).Returns(() => Task.FromResult(temperature));
        _sensorStateRepository.Setup(tr => tr.Save(It.IsAny<State>())).Returns(() => Task.FromResult(1));
        _retrieveSensorState = new RetrieveSensorState(_captor.Object,_sensorStateRepository.Object);
    }

    [When(@"we try to retrieve the sensor state")]
    public async void WhenWeTryToRetrieveTheSensorState()
    {
        _state = await _retrieveSensorState.Execute();
    }

    [Then(@"the state value should be '(.*)'")]
    public void ThenTheStateValueShouldBe(string state)
    {
        Assert.AreEqual(state, _state);
    }

    [Then(@"the state will be saved")]
    public void ThenTheStateWillBeSaved()
    {
        _sensorStateRepository.Verify(tr=> tr.Save(It.IsAny<State>()), Times.Once());
    }
}
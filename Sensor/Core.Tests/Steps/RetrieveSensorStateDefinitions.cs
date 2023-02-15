using Core.SpiPort;
using Core.ApiPort;
using Core.UseCase;
using Moq;
using NUnit.Framework;

namespace Core.Tests.Steps;

[Binding]
public sealed class RetrieveSensorStateDefinitions
{
    private readonly Mock<ICaptorPort> _captor;
    private IRetrieveSensorState _retrieveSensorState;
    private readonly Mock<ITemperatureRepositoryPort> _temperatureRepository; 
    private string _state;
    private int _temperature;
    
    public RetrieveSensorStateDefinitions()
    {
        _captor = new Mock<ICaptorPort>();
        _temperatureRepository = new Mock<ITemperatureRepositoryPort>();
    }

    [Given(@"temperature value is equal to '(.*)'")]
    public void GivenTemperatureValueIsEqualTo(int temperature)
    {
        _temperature = temperature;
        _captor.Setup(c => c.GetTemperature()).Returns(() => Task.FromResult(temperature));
        _temperatureRepository.Setup(tr => tr.Save(temperature)).Returns(() => Task.FromResult(1));
        _retrieveSensorState = new RetrieveSensorState(_captor.Object,_temperatureRepository.Object);
    }

    [When(@"we try to retrieve the sensor state")]
    public async void WhenWeTryToRetrieveTheSensorState()
    {
        _state = await _retrieveSensorState.Execute();
    }

    [Then(@"the state should be '(.*)'")]
    public void ThenTheStateShouldBe(string state)
    {
        Assert.AreEqual(state, _state);
    }

    [Then(@"the temperature will be saved")]
    public void ThenTheTemperatureWillBeSaved()
    {
        _temperatureRepository.Verify(tr=> tr.Save(_temperature), Times.Once());
    }
}
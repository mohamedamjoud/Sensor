using Core.SpiPort;
using Core.ApiPort;
using Core.Domain.Sensor;
using Core.UseCase;
using Moq;
using NUnit.Framework;

namespace Core.Tests.Steps;

[Binding]
public sealed class RetrieveSensorDefinitions
{
    private readonly Mock<ITemperatureSensorPort> _captor;
    private IRetrieveSensor _retrieveSensor;
    private readonly Mock<ISensorRepositoryPort> _sensorRepository; 
    private string _state;
    
    public RetrieveSensorDefinitions()
    {
        _captor = new Mock<ITemperatureSensorPort>();
        _sensorRepository = new Mock<ISensorRepositoryPort>();
        
        _sensorRepository.Setup(tr => tr.Save(It.IsAny<Sensor>())).
            Returns(() => Task.FromResult(1));
    }

    [Given(@"temperature value is equal to '(.*)'")]
    public void GivenTemperatureValueIsEqualTo(float temperature)
    {
        MockCaptor(temperature);
        _retrieveSensor = new RetrieveSensor(_captor.Object,_sensorRepository.Object,SensorIntervalsHelper.Value);
    }

    [When(@"we try to retrieve the sensor state")]
    public async void WhenWeTryToRetrieveTheSensorState()
    {
        _state = await _retrieveSensor.Execute();
    }

    [Then(@"the state value should be '(.*)'")]
    public void ThenTheStateValueShouldBe(string state)
    {
        Assert.AreEqual(state, _state);
    }

    [Then(@"the state will be saved")]
    public void ThenTheStateWillBeSaved()
    {
       _sensorRepository.Verify(tr=> tr.Save(It.IsAny<Sensor>()), Times.Once());
    }
    
    public void MockCaptor(float temperature)
    {
        _captor.Setup(c => c.GetTemperature()).Returns(() => temperature);
    }
}
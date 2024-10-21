using Core.SpiPort;
using Core.ApiPort;
using Core.Domain.Sensor;
using Core.Exception;
using Core.UseCase;
using Moq;
using NUnit.Framework;
namespace Core.Tests.Steps;

[Binding]
public sealed class RetrieveSensorDefinitions
{
    private Mock<ITemperatureSensorPort> _captor;
    private Mock<ISensorIntervalProvider> _sensorIntervalProvider; 
    private Mock<ISensorRepositoryPort> _sensorRepository;

    private IRetrieveSensor _retrieveSensor;
    private string _state;
    private System.Exception _capturedException;

    [BeforeScenario]
    public void SetUp()
    {
        _captor = new Mock<ITemperatureSensorPort>();
        _sensorIntervalProvider = new Mock<ISensorIntervalProvider>();
        _sensorRepository = new Mock<ISensorRepositoryPort>();
        
        _sensorIntervalProvider.Setup(sip => sip.GetIntervals()).Returns(SensorIntervalsHelper.Value);
        _sensorRepository.Setup(tr => tr.Save(It.IsAny<Sensor>())).
            Returns(() => Task.FromResult(1));
    }

    [Given(@"temperature value is equal to '(.*)'")]
    public void GivenTemperatureValueIsEqualTo(float temperature)
    {
        _captor.Setup(c => c.GetTemperature()).Returns(() => temperature);
        _retrieveSensor = new RetrieveSensor(_captor.Object,_sensorRepository.Object, _sensorIntervalProvider.Object);
    }

    [When(@"we try to retrieve the sensor state")]
    public async Task WhenWeTryToRetrieveTheSensorState()
    {
        try
        {
            _state = await _retrieveSensor.Execute();
        }
        catch (System.Exception ex)
        {
            _capturedException = ex;
        }
    }

    [Then(@"the state value should be '(.*)'")]
    public void ThenTheStateValueShouldBe(string state)
    {
        Assert.AreEqual(state, _state);
    }

    [Then(@"the Sensor Data Should Be Saved")]
    public void ThenTheSensorDataShouldBeSaved()
    {
       _sensorRepository.Verify(tr=> tr.Save(It.IsAny<Sensor>()), Times.Once());
    }

    [Given(@"the sensor repository throws an exception")]
    public void GivenTheSensorRepositoryThrowsAnException()
    {
        _sensorRepository.Setup(tr => tr.Save(It.IsAny<Sensor>())).
            ThrowsAsync(new System.Exception("Database unavailable"));
    }

    [Then(@"a ""(.*)"" should be thrown")]
    public async Task ThenAShouldBeThrown(string exceptionName)
    {
        Assert.IsInstanceOf(typeof(SaveSensorDataException), _capturedException);
        Assert.AreEqual(exceptionName, _capturedException.GetType().Name);
    }
}
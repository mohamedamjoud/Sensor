using Core.Domain.Sensor;
using Core.SpiPort;
using Core.UseCase;
using Moq;
using NUnit.Framework;

namespace Core.Tests.Steps;
[Binding]
public class RetrieveLatestSensorRequests
{
    private IRetrieveLatestSensorRequests _retrieveLatestSensorRequests;
    private readonly Mock<ISensorIntervalProvider> _sensorIntervalProvider;
    private readonly Mock<ISensorRepositoryPort> _sensorRepository;
    private List<Sensor> _result;
    private List<(float temperature, DateTime timeStamp)> _mockData;
    public RetrieveLatestSensorRequests()
    {
        _sensorIntervalProvider = new Mock<ISensorIntervalProvider>();
        _sensorRepository = new Mock<ISensorRepositoryPort>();
        
        _sensorIntervalProvider.Setup(sip => sip.GetIntervals()).Returns(SensorIntervalsHelper.Value);
    }
    
    [Given(@"a request for the latest '(.*)' sensor state records")]
    public void GivenIAmAdapterINeedLatestSensorStatesRequests(int size)
    {
        _mockData = new List<(float temperature, DateTime timeStamp)>
        {
            (30.5f, DateTime.Now.AddMinutes(-5)),
            (28.1f, DateTime.Now.AddMinutes(-10))
        };
        
        _sensorRepository
            .Setup(str => str.RetrieveLatestSensorRequests(size))
            .Returns(Task.FromResult(_mockData));
        
        _retrieveLatestSensorRequests = new UseCase.RetrieveLatestSensorRequests(_sensorRepository.Object, _sensorIntervalProvider.Object);
    }
    
    [When(@"the system retrieves the latest '(.*)' sensor states")]
    public async Task WhenWeTryToRetrieveTheLatestFifteenSensorStatesRequests(int size)
    {
        _result = await _retrieveLatestSensorRequests.Execute(size);
    }
    
    [Then(@"the system should retrieve exactly '(.*)' sensor states")]
    public void ThenTheSensorStateRepositoryShouldBeInvokedWithSizeInParameters(int size)
    {
        _sensorRepository.Verify(str=> str.RetrieveLatestSensorRequests(size), Times.Once());
    }

    [Then(@"the result should contain the expected number of sensors")]
    public void ThenTheResultShouldContainNSensors()
    {
        Assert.AreEqual(_mockData.Count, _result.Count);
    }
}
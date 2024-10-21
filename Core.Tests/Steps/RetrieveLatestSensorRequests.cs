using Core.ApiPort;
using Core.Domain.Sensor;
using Core.SpiPort;
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

    public RetrieveLatestSensorRequests()
    {
        _sensorIntervalProvider = new Mock<ISensorIntervalProvider>();
        _sensorRepository = new Mock<ISensorRepositoryPort>();
        
        _sensorIntervalProvider.Setup(sip => sip.GetIntervals()).Returns(SensorIntervalsHelper.Value);
    }
    
    [Given(@"I am adapter, I need latest '(.*)' sensor states requests")]
    public void GivenIAmAdapterINeedLatestSensorStatesRequests(int size)
    {
        var mockData = new List<(float temperature, DateTime timeStamp)>
        {
            (30.5f, DateTime.Now.AddMinutes(-5)),
            (28.1f, DateTime.Now.AddMinutes(-10))
        };
        
        _sensorRepository
            .Setup(str => str.RetrieveLatestSensorRequests(size))
            .Returns(Task.FromResult(mockData));
        
        _retrieveLatestSensorRequests = new UseCase.RetrieveLatestSensorRequests(_sensorRepository.Object, _sensorIntervalProvider.Object);
    }
    
    [When(@"We try to retrieve the latest '(.*)' sensor states requests.")]
    public async Task WhenWeTryToRetrieveTheLatestFifteenSensorStatesRequests(int size)
    {
        _result = await _retrieveLatestSensorRequests.Execute(size);
    }
    
    [Then(@"The sensor state repository should be invoked with size '(.*)' in parameters")]
    public void ThenTheSensorStateRepositoryShouldBeInvokedWithSizeInParameters(int size)
    {
        _sensorRepository.Verify(str=> str.RetrieveLatestSensorRequests(size), Times.Once());
    }

    [Then(@"The result should contain '(.*)' sensors")]
    public void ThenTheResultShouldContainNSensors(int count)
    {
        Assert.AreEqual(count, _result.Count);
    }
}
using Core.ApiPort;
using Core.SpiPort;
using Moq;

namespace Core.Tests.Steps;
[Binding]
public class RetrieveLatestSensorRequests
{
    private IRetrieveLatestSensorRequests _retrieveLatestSensorRequests;
    private readonly Mock<ISensorIntervalProvider> _sensorIntervalProvider;
    private readonly Mock<ISensorRepositoryPort> _sensorRepository;
    
    public RetrieveLatestSensorRequests()
    {
        _sensorIntervalProvider = new Mock<ISensorIntervalProvider>();
        _sensorRepository = new Mock<ISensorRepositoryPort>();
        
        _sensorIntervalProvider.Setup(sip => sip.GetIntervals()).Returns(SensorIntervalsHelper.Value);
    }
    
    [Given(@"I am adapter, I need latest '(.*)' sensor states requests")]
    public void GivenIAmAdapterINeedLatestSensorStatesRequests(int size)
    {
        _sensorRepository
            .Setup(str => str.RetrieveLatestSensorRequests(size))
            .Returns(Task.FromResult(new List<(float temperature,DateTime timeStamp)>()));
        
        _retrieveLatestSensorRequests = new UseCase.RetrieveLatestSensorRequests(_sensorRepository.Object, _sensorIntervalProvider.Object);
    }
    
    [When(@"We try to retrieve the latest '(.*)' sensor states requests.")]
    public void WhenWeTryToRetrieveTheLatestFifteenSensorStatesRequests(int size)
    {
        _retrieveLatestSensorRequests.Execute(size);
    }
    
    [Then(@"The sensor state repository should be invoked with size '(.*)' in parameters")]
    public void ThenTheSensorStateRepositoryShouldBeInvokedWithSizeInParameters(int size)
    {
        _sensorRepository.Verify(str=> str.RetrieveLatestSensorRequests(size), Times.Once());
    }


}
using Core.ApiPort;
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
    private readonly Mock<ISensorRepositoryPort> _sensorRepository;
    public RetrieveLatestSensorRequests()
    {
        _sensorRepository = new Mock<ISensorRepositoryPort>();
    }
    
    [Given(@"I am adapter, I need latest '(.*)' sensor states requests")]
    public void GivenIAmAdapterINeedLatestSensorStatesRequests(sbyte size)
    {
        _sensorRepository
            .Setup(str => str.RetrieveLatestSensorRequests(size, SensorIntervalsHelper.Value))
            .Returns(Task.FromResult(new List<Sensor>()));
        
        _retrieveLatestSensorRequests = new UseCase.RetrieveLatestSensorRequests(_sensorRepository.Object, SensorIntervalsHelper.Value);
    }
    
    [When(@"We try to retrieve the latest Fifteen Sensor States Requests\.")]
    public void WhenWeTryToRetrieveTheLatestFifteenSensorStatesRequests(sbyte size)
    {
        _retrieveLatestSensorRequests.Execute(size);
    }
    
    [Then(@"The sensor state repository should be invoked with size '(.*)' in parameters")]
    public void ThenTheSensorStateRepositoryShouldBeInvokedWithSizeInParameters(sbyte size)
    {
        _sensorRepository.Verify(str=> str.RetrieveLatestSensorRequests(size, SensorIntervalsHelper.Value), Times.Once());
    }


}
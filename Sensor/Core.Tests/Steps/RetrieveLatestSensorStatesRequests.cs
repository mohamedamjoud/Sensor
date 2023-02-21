using Core.ApiPort;
using Core.Domain.Sensor;
using Core.SpiPort;
using Core.UseCase;
using Moq;
using NUnit.Framework;

namespace Core.Tests.Steps;

[Binding]
public class RetrieveLatestSensorStatesRequests
{
    private IRetrieveLatestRequestsStates _retrieveLatestRequestsStates;
    private readonly Mock<ISensorStateRepositoryPort> _sensorStateRepository;
    public RetrieveLatestSensorStatesRequests()
    {
        _sensorStateRepository = new Mock<ISensorStateRepositoryPort>();
    }
    
    [Given(@"I am adapter, I need latest '(.*)' sensor states requests")]
    public void GivenIAmAdapterINeedLatestSensorStatesRequests(sbyte size)
    {
        _sensorStateRepository.Setup(str => str.GetLatestRequestsStates(size))
            .Returns(Task.FromResult(new List<State>()));
        _retrieveLatestRequestsStates = new RetrieveLatestRequestsStates(_sensorStateRepository.Object);
    }
    
    [When(@"We try to retrieve the latest Fifteen Sensor States Requests\.")]
    public void WhenWeTryToRetrieveTheLatestFifteenSensorStatesRequests()
    {
        _retrieveLatestRequestsStates.Execute();
    }
    
    [Then(@"The sensor state repository should be invoked with size '(.*)' in parameters")]
    public void ThenTheSensorStateRepositoryShouldBeInvokedWithSizeInParameters(sbyte size)
    {
        _sensorStateRepository.Verify(str=> str.GetLatestRequestsStates(size), Times.Once());
    }


}
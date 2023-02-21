using Core.ApiPort;
using Core.Domain.Sensor;
using Core.SpiPort;
using Core.UseCase;
using Moq;

namespace Core.Tests.Steps;

public class SensorStateContext
{
    private readonly Mock<ICaptorPort> _captor;
    public IRetrieveSensorState RetrieveSensorState { get; private set; }
    public  Mock<ISensorStateRepositoryPort> SensorStateRepository { get; private set; }

    public SensorStateContext()
    {
        _captor = new Mock<ICaptorPort>();
        SensorStateRepository = new Mock<ISensorStateRepositoryPort>();
        SensorStateRepository.Setup(tr => tr.Save(It.IsAny<State>())).Returns(() => Task.FromResult(1));
    }

    public void MockServices(sbyte temperature)
    {
        _captor.Setup(c => c.GetTemperature()).Returns(() => Task.FromResult(temperature));
        RetrieveSensorState = new RetrieveSensorState(_captor.Object,SensorStateRepository.Object); 
    }
}
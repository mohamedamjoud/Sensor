using Adapter.SQLLit;
using Adapter.SQLLit.Models;
using Adapter.SQLLit.Repository;
using Core.Domain.Sensor;
using Core.SpiPort;
using Moq;

namespace Adapter.SQLit.Tests;

public class SensorStateRepositoryTests
{
    private State _state;
    private ISensorStateRepositoryPort _unitOfWork;
    private Mock<IRepository<SensorState>> _sensorRepository;
    
    [SetUp]
    public void Setup()
    {
        _sensorRepository = new Mock<IRepository<SensorState>>();
        _sensorRepository.Setup(sr=>sr.Add(It.IsAny<SensorState>())).Returns(It.IsAny<Task<int>>());
        _unitOfWork = new UnitOfWork(_sensorRepository.Object);
    }

    [Test]
    public void Add_SaveTemperature_MethodInvoked()
    {
        //Arrange
        _state = new State(1);
        
        //Act
        _unitOfWork.Save(_state);
        
        //Assert
        _sensorRepository.Verify(r => r.Add(It.IsAny<SensorState>()), Times.Once);
    }
}
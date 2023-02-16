using Adapter.SQLLit.Context;
using Adapter.SQLLit.Models;
using Adapter.SQLLit.Repository;
using Core.Domain.Sensor;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Adapter.SQLLit.Tests.Steps;

[Binding]
public sealed class SaveTemperatureRequestStepDefinitions
{
    private State _state;
    private IUnitOfWork _unitOfWork;
    private Mock<IRepository<SensorState>> _sensorRepository;
    
    public SaveTemperatureRequestStepDefinitions()
    {
        _sensorRepository = new Mock<IRepository<SensorState>>();
        _sensorRepository.Setup(sr=>sr.Add(It.IsAny<SensorState>())).Returns(It.IsAny<Task<int>>());
        _unitOfWork = new UnitOfWork(_sensorRepository.Object);
    }
    
    [Given(@"sensor state value '(.*)'")]
    public void GivenSensorStateValue(int temperature)
    {
        _state = new State(temperature);
    }

    [When(@"we try to save the sensor state")]
    public void WhenWeTryToSaveTheSensorState()
    {
        _unitOfWork.Save(_state);
    }

    [Then(@"the sensor state will be saved")]
    public void ThenTheSensorStateWillBeSaved()
    {
        _sensorRepository.Verify(r => r.Add(It.IsAny<SensorState>()), Times.Once);
    }
}
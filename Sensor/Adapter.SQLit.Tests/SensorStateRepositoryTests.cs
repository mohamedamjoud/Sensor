using Adapter.SQLLit;
using Adapter.SQLLit.Context;
using Adapter.SQLLit.Models;
using Adapter.SQLLit.Repository;
using Core.Domain.Sensor;
using Core.SpiPort;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Adapter.SQLit.Tests;

public class SensorStateRepositoryTests
{
    private State _state;
    private Mock<ISensorStateRepository> _sensorStateRepositoryMock;
    
    [SetUp]
    public void Setup()
    {
        _sensorStateRepositoryMock = new Mock<ISensorStateRepository>();
        _sensorStateRepositoryMock.Setup(sr=>sr.Save(It.IsAny<State>())).Returns(It.IsAny<Task<int>>());
       
        
    }

    [Test]
    public void Add_SaveTemperature_MethodInvoked()
    {
        //Arrange
        _state = new State(1);
        
        //Act
        _sensorStateRepositoryMock.Object.Save(_state);
        
        //Assert
        _sensorStateRepositoryMock.Verify(r => r.Save(It.IsAny<State>()), Times.Once);
    }

    [TestCase(12, 12)]
    [TestCase(5, 5)]
    public async Task GetAll_RetrieveNLatestSensorState_MethodeInvoked(int size,int expectedElement)
    {
        //Arrange
        var fakeSensorStates = GetFakeSensorStates();;
        
        var temperatureContextMock = new Mock<TemperatureContext>();
        temperatureContextMock.Setup<DbSet<SensorState>>(ss => ss.SensorState)
            .ReturnsDbSet(fakeSensorStates.ToList());
        
        SensorStateRepository sensorStateRepository = new SensorStateRepository(temperatureContextMock.Object);
        
        
        //Act
        var retrievedRequestsStates =  await sensorStateRepository.GetLatestRequestsStates(size);

        //Assert
        Assert.AreEqual(retrievedRequestsStates.Count, expectedElement);
        Assert.AreEqual(fakeSensorStates.OrderByDescending(ss=>ss.DateTime).Take(size).ToList().Select(ss => State.Of(ss.DateTime,ss.Value)).ToList(), retrievedRequestsStates);
    }

    private List<SensorState> GetFakeSensorStates()
    {
        const int dayMin = 1;
        const int dayMax = 31;
        const int month = 01;
        const int year = 2022;

        return new List<SensorState>()
        {
            new SensorState(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new SensorState(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new SensorState(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new SensorState(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new SensorState(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new SensorState(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new SensorState(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new SensorState(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new SensorState(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new SensorState(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new SensorState(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new SensorState(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new SensorState(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new SensorState(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new SensorState(1, new DateTime(year,month,new Random().Next(dayMin,dayMax)))
        };
    }
}
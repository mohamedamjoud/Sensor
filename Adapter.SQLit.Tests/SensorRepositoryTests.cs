using Adapter.SQLLit.Context;
using Adapter.SQLLit.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Sensor = Adapter.SQLLit.Models.Sensor;

namespace Adapter.SQLit.Tests;

public class SensorRepositoryTests
{
    private Core.Domain.Sensor.Sensor _sensor;
    private Mock<ISensorRepository> _sensorRepositoryMock;
    
    [SetUp]
    public void Setup()
    {
        _sensorRepositoryMock = new Mock<ISensorRepository>();
        _sensorRepositoryMock.Setup(sr=>
            sr.Save(It.IsAny<Core.Domain.Sensor.Sensor>())).Returns(It.IsAny<Task<int>>());
    }

    [Test]
    public void Add_SaveTemperature_SaveMethodInvoked()
    {
        //Arrange
        _sensor = new Core.Domain.Sensor.Sensor(1,SensorIntervalsHelper.Value);
        
        //Act
        _sensorRepositoryMock.Object.Save(_sensor);
        
        //Assert
        _sensorRepositoryMock.Verify(r => 
            r.Save(It.IsAny<Core.Domain.Sensor.Sensor>()), Times.Once);
    }

    [TestCase(12)]
    [TestCase(5)]
    public async Task GetAll_RetrieveNLatestSensorState_MethodeInvoked(int size)
    {
        //Arrange
        var fakeSensorStates = GetFakeSensorStates();;
        
        var temperatureContextMock = new Mock<TemperatureContext>();
        temperatureContextMock.Setup<DbSet<Sensor>>(ss => ss.Sensors)
            .ReturnsDbSet(fakeSensorStates.ToList());
        
        SensorRepository sensorRepository = new SensorRepository(temperatureContextMock.Object);
        
        //Act
        var retrievedRequestsStates =  await sensorRepository.RetrieveLatestSensorRequests(size,SensorIntervalsHelper.Value);

        //Assert
        Assert.AreEqual(retrievedRequestsStates.Count, size);
    }

    private List<Sensor> GetFakeSensorStates()
    {
        const int dayMin = 1;
        const int dayMax = 31;
        const int month = 01;
        const int year = 2022;

        return new List<Sensor>()
        {
            new Sensor(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new Sensor(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new Sensor(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new Sensor(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new Sensor(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new Sensor(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new Sensor(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new Sensor(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new Sensor(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new Sensor(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new Sensor(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new Sensor(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new Sensor(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new Sensor(1, new DateTime(year,month,new Random().Next(dayMin,dayMax))),
            new Sensor(1, new DateTime(year,month,new Random().Next(dayMin,dayMax)))
        };
    }
}
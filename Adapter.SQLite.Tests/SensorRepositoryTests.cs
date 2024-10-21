using Adapter.SQLite.Context;
using Adapter.SQLite.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Sensor = Adapter.SQLite.Models.Sensor;

namespace Adapter.SQLite.Tests;

public class SensorRepositoryTests
{
    private Core.Domain.Sensor.Sensor _sensor;
    private Mock<ISensorRepository> _sensorRepositoryMock;
    [SetUp]
    public void Setup()
    {
        _sensorRepositoryMock = new Mock<ISensorRepository>();
        _sensorRepositoryMock.Setup(sr=> sr.Save(It.IsAny<Core.Domain.Sensor.Sensor>())).ReturnsAsync(1);
    }

    [Test]
    public async Task Add_SaveTemperature_CallsSaveMethodOnce ()
    {
        //Arrange
        _sensor = new Core.Domain.Sensor.Sensor(1,SensorIntervalsHelper.  Value);
        
        //Act
        await _sensorRepositoryMock.Object.Save(_sensor);
        
        //Assert
        _sensorRepositoryMock.Verify(r => r.Save(_sensor), Times.Once);
    }

    [TestCase(12)]
    [TestCase(5)]
    public async Task GetAll_RetrieveNLatestSensorState_MethodInvoked(int size)
    {
        //Arrange
        var fakeSensorStates = GetFakeSensorStates();;
        
        var options = new DbContextOptionsBuilder<TemperatureContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var temperatureContextMock = new Mock<TemperatureContext>(options);

        temperatureContextMock.Setup<DbSet<Sensor>>(ss => ss.Sensors)
            .ReturnsDbSet(fakeSensorStates.ToList());
        
        SensorRepository sensorRepository = new SensorRepository(temperatureContextMock.Object);
        
        //Act
        var retrievedRequestsStates =  await sensorRepository.RetrieveLatestSensorRequests(size);

        //Assert
        Assert.AreEqual(size, retrievedRequestsStates.Count);
    }

    private List<Sensor> GetFakeSensorStates()
    {
        var baseDate = new DateTime(2022, 01, 01);
        return Enumerable.Range(0, 15).Select(i => new Sensor(1, baseDate.AddDays(i))).ToList();
    }
}
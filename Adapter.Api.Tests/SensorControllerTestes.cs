using Adapter.Api.Controllers;
using Core.ApiPort;
using Core.Domain.Sensor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using ILogger = Castle.Core.Logging.ILogger;

namespace Adapter.Api.Tests;

public class SensorControllerTestes
{
    private const int Size = 15;
    private readonly Mock<ILogger<SensorController>> _loggerMock;
    private SensorController _sensorController;

    public SensorControllerTestes()
    {
        _loggerMock = new Mock<ILogger<SensorController>>();
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task GetSensorState_ShouldReturnOkResultType()
    {
        //Arrange
        var retrieveState = new Mock<IRetrieveSensor>();
        retrieveState.Setup(rss => rss.Execute()).Returns(Task.FromResult("Hot"));
        
        var retrieveLatestSensorRequests = new Mock<IRetrieveLatestSensorRequests>();
        
        _sensorController =  new SensorController(
            retrieveState.Object,
            retrieveLatestSensorRequests.Object,
            WebApplication.CreateBuilder().Configuration,
            _loggerMock.Object);
        //Act
        var response = await _sensorController.Get();
        
        //Assert
        var statusCode = ((response as IStatusCodeActionResult)!).StatusCode;
        Assert.That( statusCode, Is.EqualTo(StatusCodes.Status200OK));
    }
    
    [Test]
    public async Task GetSensorState_ExceptionThrown_ShouldReturnInternalServerError()
    {
        //Arrange
        var retrieveState = new Mock<IRetrieveSensor>();
        retrieveState.Setup(rss => rss.Execute()).Throws(new Exception());
        
        var retrieveLatestSensorRequests = new Mock<IRetrieveLatestSensorRequests>();
        
        _sensorController =  new SensorController(
            retrieveState.Object,
            retrieveLatestSensorRequests.Object,
            WebApplication.CreateBuilder().Configuration,
            _loggerMock.Object);
        
        //Act
        var response = await _sensorController.Get();

        //Assert
        var statusCode = ((response as IStatusCodeActionResult)!).StatusCode;
        Assert.That( statusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
    }
    
    [Test]
    public async Task RetrieveLatestSensorRequests_ShouldReturnOkResultType()
    {
        //Arrange
        var retrieveState = new Mock<IRetrieveSensor>();
        
        var retrieveLatestSensorRequests = new Mock<IRetrieveLatestSensorRequests>();
        retrieveLatestSensorRequests.Setup(rss => 
            rss.Execute(Size)).Returns(Task.FromResult(new List<Sensor>()));
        
        _sensorController =  new SensorController(
            retrieveState.Object,
            retrieveLatestSensorRequests.Object,
            WebApplication.CreateBuilder().Configuration,
            _loggerMock.Object);
        
        //Act
        var response = await _sensorController.Get(Size);
        
        //Assert
        var statusCode = ((response as IStatusCodeActionResult)!).StatusCode;
        Assert.That( statusCode, Is.EqualTo(StatusCodes.Status200OK));
    }
    
    [Test]
    public async Task RetrieveLatestSensorRequests_ExceptionThrown_ShouldReturnInternalServerError()
    {
        //Arrange
        var retrieveState = new Mock<IRetrieveSensor>();
        
        var retrieveLatestSensorRequests = new Mock<IRetrieveLatestSensorRequests>();
        retrieveLatestSensorRequests.Setup(rss => rss.Execute(Size)).Throws(new Exception());
        
        _sensorController =  new SensorController(
            retrieveState.Object,
            retrieveLatestSensorRequests.Object,
            WebApplication.CreateBuilder().Configuration,
            _loggerMock.Object);
        
        //Act
        var response = await _sensorController.Get(Size);

        //Assert
        var statusCode = ((response as IStatusCodeActionResult)!).StatusCode;
        Assert.That( statusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
    }
}
using Adapter.Api.Controllers;
using Core.ApiPort;
using Core.Domain.Sensor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;

namespace Adapter.Api.Tests;

public class SensorControllerTests
{
    private const int Size = 15;
    private readonly Mock<ILogger<SensorController>> _loggerMock;
    private SensorController _sensorController;

    public SensorControllerTests()
    {
        _loggerMock = new Mock<ILogger<SensorController>>();
    }

    private void SetupController(Mock<IRetrieveSensor> mockRetrieveState,
        Mock<IRetrieveLatestSensorRequests> mockRetrieveLatestSensorRequests)
    {
        _sensorController = new SensorController(
            mockRetrieveState.Object,
            mockRetrieveLatestSensorRequests.Object,
            WebApplication.CreateBuilder().Configuration,
            _loggerMock.Object);
    }
    
    [Test]
    public async Task GetSensorState_WhenCalled_ReturnsOkResult()
    {
        //Arrange
        var mockRetrieveState = new Mock<IRetrieveSensor>();
        mockRetrieveState.Setup(rss => rss.Execute()).Returns(Task.FromResult("Hot"));
        
        var mockRetrieveLatestSensorRequests = new Mock<IRetrieveLatestSensorRequests>();
        
        SetupController(mockRetrieveState, mockRetrieveLatestSensorRequests);

        //Act
        var response = await _sensorController.Get();
        
        //Assert
        Assert.That(response, Is.InstanceOf<OkObjectResult>());
        var okResult = response as OkObjectResult;
        Assert.That(okResult?.Value, Is.Not.Null);
        var statusCode = okResult?.StatusCode;
        Assert.That(statusCode, Is.EqualTo(StatusCodes.Status200OK));
    }
    
    [Test]
    public async Task GetSensorState_ExceptionThrown_ShouldReturnInternalServerError()
    {
        //Arrange
        var mockRetrieveState = new Mock<IRetrieveSensor>();
        mockRetrieveState.Setup(rss => rss.Execute()).Throws(new Exception());
        
        var mockRetrieveLatestSensorRequests = new Mock<IRetrieveLatestSensorRequests>();
        
        SetupController(mockRetrieveState, mockRetrieveLatestSensorRequests);
        
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
        var mockRetrieveState = new Mock<IRetrieveSensor>();
        var mockRetrieveLatestSensorRequests = new Mock<IRetrieveLatestSensorRequests>();
        
        mockRetrieveLatestSensorRequests.Setup(rss => 
            rss.Execute(Size)).Returns(Task.FromResult(new List<Sensor>()));
        
        SetupController(mockRetrieveState, mockRetrieveLatestSensorRequests);
        
        //Act
        var response = await _sensorController.Get(Size);
        
        //Assert
        Assert.That(response, Is.InstanceOf<OkObjectResult>());
        var okResult = response as OkObjectResult;
        Assert.That(okResult?.Value, Is.Not.Null);
        var statusCode = okResult?.StatusCode;
        Assert.That(statusCode, Is.EqualTo(StatusCodes.Status200OK));
    }
    
    [Test]
    public async Task RetrieveLatestSensorRequests_ExceptionThrown_ShouldReturnInternalServerError()
    {
        //Arrange
        var mockRetrieveState = new Mock<IRetrieveSensor>();
        var mockRetrieveLatestSensorRequests = new Mock<IRetrieveLatestSensorRequests>();
        
        SetupController(mockRetrieveState, mockRetrieveLatestSensorRequests);
        
        //Act
        var response = await _sensorController.Get(Size);

        //Assert
        var statusCode = ((response as IStatusCodeActionResult)!).StatusCode;
        Assert.That( statusCode, Is.EqualTo(StatusCodes.Status500InternalServerError));
    }
}
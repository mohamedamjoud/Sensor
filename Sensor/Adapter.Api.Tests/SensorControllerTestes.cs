using Adapter.Api.Controllers;
using Core.ApiPort;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;

namespace Adapter.Api.Tests;

public class SensorControllerTestes
{
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task GetSensorState_ShouldReturnOkResultType()
    {
        //Arrange
        var retriveSensorState = new Mock<IRetrieveSensorState>();
        retriveSensorState.Setup(rss => rss.Execute()).Returns(Task.FromResult("Hot"));
        var sensorController = new SensorController(retriveSensorState.Object);
        
        //ActS
        var response = await sensorController.Get();
        
        //Assert
        var statusCode = ((response as IStatusCodeActionResult)!).StatusCode;
        Assert.AreEqual( StatusCodes.Status200OK,statusCode);
    }
    
    [Test]
    public async Task GetSensorState_ExceptionThrown_ShouldReturnInternalServerError()
    {
        //Arrange
        var retriveSensorState = new Mock<IRetrieveSensorState>();
        retriveSensorState.Setup(rss => rss.Execute()).Throws(new Exception());
        var sensorController = new SensorController(retriveSensorState.Object);
        
        //Act
        var response = await sensorController.Get();

        //Assert
        var statusCode = ((response as IStatusCodeActionResult)!).StatusCode;
        Assert.AreEqual( StatusCodes.Status500InternalServerError,statusCode);
    }
}
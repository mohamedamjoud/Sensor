using Adapter.Api.Controllers;
using Core.ApiPort;
using Core.Domain.Sensor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using NUnit.Framework;

namespace Adapter.Api.Tests;

public class RequestStateControllerTestes
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task RetrieveRequestsStates_ShouldReturnOkResultType()
    {
        //Arrange
        var retrieveLatestRequestsStates = new Mock<IRetrieveLatestRequestsStates>();
        retrieveLatestRequestsStates.Setup(rss => rss.Execute()).Returns(Task.FromResult(new List<State>()));
        var requestStateController = new RequestStateController(retrieveLatestRequestsStates.Object);
        
        //ActS
        var response = await requestStateController.Get();
        
        //Assert
        var statusCode = ((response as IStatusCodeActionResult)!).StatusCode;
        Assert.AreEqual( StatusCodes.Status200OK,statusCode);
    }
    
    [Test]
    public async Task RetrieveRequestsStates_ExceptionThrown_ShouldReturnInternalServerError()
    {
        //Arrange

        var retrieveLatestRequestsStates = new Mock<IRetrieveLatestRequestsStates>();
        retrieveLatestRequestsStates.Setup(rss => rss.Execute()).Throws(new Exception());
        var requestStateController = new RequestStateController(retrieveLatestRequestsStates.Object);
        //Act
        var response = await requestStateController.Get();

        //Assert
        var statusCode = ((response as IStatusCodeActionResult)!).StatusCode;
        Assert.AreEqual( StatusCodes.Status500InternalServerError,statusCode);
    }
}
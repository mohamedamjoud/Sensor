using Core.ApiPort;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RequestStateController : ControllerBase
{
    private readonly IRetrieveLatestRequestsStates _retrieveLatestRequestsStates;

    public RequestStateController(IRetrieveLatestRequestsStates retrieveLatestRequestsStates)
    {
        _retrieveLatestRequestsStates = retrieveLatestRequestsStates;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var requestsStates = await _retrieveLatestRequestsStates.Execute();
            var response =requestsStates.Select(rs => new Sensor() { State = rs.Name, Date = rs.DateTime }).ToList();
           
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
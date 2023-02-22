using Core.ApiPort;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SensorController : ControllerBase
{
    private readonly IRetrieveSensorState _retrieveSensorState;
    public SensorController(IRetrieveSensorState retrieveSensorState)
    {
        _retrieveSensorState = retrieveSensorState;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
             var state = await _retrieveSensorState.Execute();
             return Ok(new Sensor{State = state});
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
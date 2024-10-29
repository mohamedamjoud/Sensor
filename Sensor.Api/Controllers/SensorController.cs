using Adapter.Api.Dto;
using Core.ApiPort;
using Microsoft.AspNetCore.Mvc;

namespace Adapter.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SensorController : ControllerBase
{
    private const string RetrieveLatestSensorRequestsSize = "RetrieveLatestSensorRequestsSize";
    private readonly IRetrieveSensor _retrieveSensor;
    private readonly IRetrieveLatestSensorRequests _retrieveLatestSensorRequests;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;
    
    public SensorController(
        IRetrieveSensor retrieveSensor, 
        IRetrieveLatestSensorRequests retrieveLatestSensorRequests, 
        IConfiguration  configuration,
        ILogger<SensorController> logger)
    {
        _retrieveSensor = retrieveSensor;
        _retrieveLatestSensorRequests = retrieveLatestSensorRequests;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var state = await _retrieveSensor.Execute();

            return Ok(new SensorResponseDto(state));
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
     
    [HttpGet]
    [Route("Search/")]
    public async Task<IActionResult> Get(int? size)
    {
        try
        {
            var configurationSize = _configuration.GetSection(RetrieveLatestSensorRequestsSize).Get<int>();
            
            var sensorRequests = await _retrieveLatestSensorRequests.Execute(size ?? configurationSize);
            
            var response = sensorRequests
                .Select(rs => new SensorResponseDto(rs.State.Value,rs.DateTime))
                .ToList();
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
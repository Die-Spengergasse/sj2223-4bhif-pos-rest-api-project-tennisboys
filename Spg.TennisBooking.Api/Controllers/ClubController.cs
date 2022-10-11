using Microsoft.AspNetCore.Mvc;

namespace Spg.TennisBooking.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClubController : ControllerBase
{
    private readonly ILogger<ClubController> _logger;

    public ClubController(ILogger<ClubController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetClubs")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = "Hey"
        })
        .ToArray();
    }
}

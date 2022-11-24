using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.MvcFrontEnd;

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

    // public IActionResult Privacy()
    // {
    //     // Q: How does it know which view to use?
    //     // A: It looks for a view with the same name as the action method.
    //     return View();
    // }
}

using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.MvcFrontEnd.Models;
using System.Diagnostics;

namespace Spg.TennisBooking.MvcFrontEnd.Controllers
{
    public class ClubsController : Controller
    {
        private readonly ILogger<ClubsController> _logger;

        public ClubsController(ILogger<ClubsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("clubs")]
        public IActionResult Index([FromQuery] string search)
        {
            _logger.LogInformation("Getting clubs {search}", search);
            return View(new { search });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.MvcFrontEnd.Models;
using System.Diagnostics;

namespace Spg.TennisBooking.MvcFrontEnd.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(ILogger<RegisterController> logger)
        {
            _logger = logger;
        }

        [HttpGet("register")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("verify")]
        public IActionResult Verify([FromQuery] string uuid)
        {
            return View(new { uuid });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
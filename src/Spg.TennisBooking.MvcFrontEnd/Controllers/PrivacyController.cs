using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.MvcFrontEnd.Models;
using System.Diagnostics;

namespace Spg.TennisBooking.MvcFrontEnd.Controllers
{
    public class PrivacyController : Controller
    {
        private readonly ILogger<PrivacyController> _logger;

        public PrivacyController(ILogger<PrivacyController> logger)
        {
            _logger = logger;
        }

        [HttpGet("privacy")]
        public IActionResult Index()
        {
            _logger.LogInformation("Privacy");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
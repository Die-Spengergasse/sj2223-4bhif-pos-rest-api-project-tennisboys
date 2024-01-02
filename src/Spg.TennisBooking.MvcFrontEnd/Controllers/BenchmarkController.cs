using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.MvcFrontEnd.Models;
using System.Diagnostics;

namespace Spg.TennisBooking.MvcFrontEnd.Controllers
{
    public class BenchmarkController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public BenchmarkController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet("benchmark")]
        public IActionResult Index()
        {
            _logger.LogInformation("account");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
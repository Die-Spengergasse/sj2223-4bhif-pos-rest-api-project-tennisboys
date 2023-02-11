using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.MvcFrontEnd.Models;
using System.Diagnostics;

namespace Spg.TennisBooking.MvcFrontEnd.Controllers
{
    public class ClubController : Controller
    {
        private readonly ILogger<ClubController> _logger;

        public ClubController(ILogger<ClubController> logger)
        {
            _logger = logger;
        }

        [HttpGet("c/{link}")]
        public IActionResult Index(string link)
        {
            _logger.LogInformation("Club {link}", link);
            return View(new { link });
        }

        [HttpGet("c/{link}/courts")]
        public IActionResult Courts(string link)
        {
            _logger.LogInformation("Courts Club {link}", link);
            return View(new { link });
        }

        [HttpGet("c/{link}/court/{court}")]
        public IActionResult Court(string link, string court)
        {
            _logger.LogInformation("Court {court} Club {link}", court, link);
            return View(new { link, court });
        }

        [HttpGet("c/{link}/events")]
        public IActionResult Events(string link)
        {
            _logger.LogInformation("Events Club {link}", link);
            return View(new { link });
        }

        [HttpGet("c/{link}/events/{event}")]
        public IActionResult Event(string link, string @event)
        {
            _logger.LogInformation("Event {event} Club {link}", @event, link);
            return View(new { link, @event });
        }

        [HttpGet("c/{link}/news")]
        public IActionResult News(string link)
        {
            _logger.LogInformation("News Club {link}", link);
            return View(new { link });
        }

        [HttpGet("c/{link}/news/{news}")]
        public IActionResult NewsItem(string link, string news)
        {
            _logger.LogInformation("NewsItem {news} Club {link}", news, link);
            return View(new { link, news });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
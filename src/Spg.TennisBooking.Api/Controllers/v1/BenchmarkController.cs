using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class BenchmarkController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ClubEventController> _logger;
        private readonly IBenchmarkService _benchmark;

        public BenchmarkController(IWebHostEnvironment env, IConfiguration configuration, ILogger<ClubEventController> logger, IBenchmarkService benchmark)
        {
            _env = env;
            _configuration = configuration;
            _logger = logger;
            _benchmark = benchmark;
        }

        [HttpGet("sql")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSQL()
        {
            try
            {
                return await _benchmark.GetSQL();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting event");
                if (_env.IsDevelopment())
                {
                    return StatusCode(500, e.Message);
                }
                else
                {
                    return StatusCode(500, "Internal Server Error");
                }
            }
        }

        [HttpGet("mongo")]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<IActionResult> GetMongo()
        {
            try
            {
                return await _benchmark.GetMongo();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while getting event");
                if (_env.IsDevelopment())
                {
                    return StatusCode(500, e.Message);
                }
                else
                {
                    return StatusCode(500, "Internal Server Error");
                }
            }
        }
    }
}

using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Spg.TennisBooking.Domain.Dtos.ClubDtos;
using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClubController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ClubController> _logger;
    private readonly IClubService _club;

    public ClubController(IWebHostEnvironment env, IConfiguration configuration, ILogger<ClubController> logger, IClubService club)
    {
        _env = env;
        _configuration = configuration;
        _logger = logger;
        _club = club;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string name)
    {
        try
        {
            IActionResult result = await _club.Get(name);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting club");
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

    [HttpPost]
    //[Authorize]
    public async Task<IActionResult> Post([FromBody] PostClubDto clubDto)
    {
        try
        {
            IActionResult result = await _club.Post(clubDto);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while posting club");
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

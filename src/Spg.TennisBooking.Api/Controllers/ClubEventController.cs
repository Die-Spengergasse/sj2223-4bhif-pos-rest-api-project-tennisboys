using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.ClubEventDtos;
using Spg.TennisBooking.Domain.Dtos.UserDtos;
using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClubEventController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ClubEventController> _logger;
    private readonly IClubEventService _clubEvent;

    public ClubEventController(IWebHostEnvironment env, IConfiguration configuration, ILogger<ClubEventController> logger, IClubEventService clubEvent)
    {
        _env = env;
        _configuration = configuration;
        _logger = logger;
        _clubEvent = clubEvent;
    }

    /* ClubEvent:
     * Get(id)
     * GetAll(Club Link)
     * Post
     * Put(id)
     * Delete(id)
     */

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            return await _clubEvent.Get(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting court");
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

    [HttpGet("club/{clubLink}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(string clubLink)
    {
        try
        {
            return await _clubEvent.GetAll(clubLink);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting court");
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
    public async Task<IActionResult> Post([FromBody] ClubEventDto clubEventDto)
    {
        try
        {
            return await _clubEvent.Post(clubEventDto, Controller.GetUserId(User));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while creating court");
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

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ClubEventDto clubEventDto)
    {
        try
        {
            return await _clubEvent.Put(clubEventDto, Controller.GetUserId(User));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while patching court");
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            return await _clubEvent.Delete(id, Controller.GetUserId(User));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting court");
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

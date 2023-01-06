using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.CourtDtos;
using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CourtController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;
    private readonly ILogger<CourtController> _logger;
    private readonly ICourtService _court;

    public CourtController(IWebHostEnvironment env, IConfiguration configuration, ILogger<CourtController> logger, ICourtService court)
    {
        _env = env;
        _configuration = configuration;
        _logger = logger;
        _court = court;
    }

    /*
     * CreateCourt
       PatchCourt
       DeleteCourt
       GetCourt
       GetAllCourts
     */

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CourtDto courtDto)
    {
        try
        {
            var court = await _court.Create(courtDto, Controller.GetUserId(User));
            return Ok(court);
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

    [HttpPatch]
    public async Task<IActionResult> Patch([FromBody] CourtDto courtDto)
    {
        try
        {
            var court = await _court.Patch(courtDto, Controller.GetUserId(User));
            return Ok(court);
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
            await _court.Delete(id, Controller.GetUserId(User));
            return Ok();
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

    [HttpGet("court/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var court = await _court.Get(id, Controller.GetUserId(User));
            return Ok(court);
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
    public async Task<IActionResult> GetAll(string clubLink)
    {
        try
        {
            var courts = await _court.GetAll(clubLink, Controller.GetUserId(User));
            return Ok(courts);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting all courts");
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

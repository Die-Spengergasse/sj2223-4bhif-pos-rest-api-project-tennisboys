using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.CourtDtos;
using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Api.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
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
    public async Task<IActionResult> Post([FromBody] PostCourtDto postCourtDto)
    {
        try
        {
            return await _court.Post(postCourtDto, Controller.GetUserId(User));
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
    public async Task<IActionResult> Put([FromBody] PutCourtDto courtDto)
    {
        try
        {
            return await _court.Put(courtDto, Controller.GetUserId(User));
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
            return await _court.Delete(id, Controller.GetUserId(User));
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

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            return await _court.Get(id);
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

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(string clubLink)
    {
        try
        {
            return await _court.GetAll(clubLink);
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

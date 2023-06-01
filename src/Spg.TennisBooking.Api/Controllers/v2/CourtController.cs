using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.CourtDtos;
using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Api.Controllers.v2;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
[Authorize]
public class CourtController : ControllerBase
{
    /// <summary>
    /// This APIController is used to do any related Court operations
    /// </summary>
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

    /// <summary>
    /// Post a Court
    /// </summary>
    /// <param name="postCourtDto"></param>
    /// <returns>return await _court.Post(postCourtDto, Controller.GetUserId(User));</returns>
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

    /// <summary>
    /// Put a Court
    /// </summary>
    /// <param name="courtDto"></param>
    /// <returns>return await _court.Put(courtDto, Controller.GetUserId(User));</returns>
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

    /// <summary>
    /// Delete a Court
    /// </summary>
    /// <param name="id"></param>
    /// <returns>return await _court.Delete(id, Controller.GetUserId(User));</returns>
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

    /// <summary>
    /// Get a Court
    /// </summary>
    /// <param name="id"></param>
    /// <returns>return await _court.Get(id);</returns>
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

    /// <summary>
    /// Get all Courts
    /// </summary>
    /// <param name="clubLink"></param>
    /// <returns>return await _court.GetAll(clubLink);</returns>
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

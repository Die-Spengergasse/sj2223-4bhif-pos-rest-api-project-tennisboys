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

    //Get
    //Post
    //Patch
    //Delete
    //PayementKey
    //Paid //as long it is over a month till end it counts as paid
        
    [HttpGet]
    public async Task<IActionResult> Get(string name)
    {
        try
        {
            return await _club.Get(name);
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
    public async Task<IActionResult> Create([FromBody] string name)
    {
        try
        {
            return await _club.Create(name);
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

    [HttpPatch]
    //[Authorize]
    public async Task<IActionResult> Patch([FromBody] PatchClubDto clubDto)
    {
        try
        {
            return await _club.Patch(clubDto);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while patching club");
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

    [HttpDelete]
    //[Authorize]
    public async Task<IActionResult> Delete([FromBody] string link)
    {
        try
        {
            return await _club.Delete(link);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting club");
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
    [Route("PayementKey")]
    public async Task<IActionResult> GetPayementKey(string link)
    {
        try
        {
            return await _club.GetPayementKey(link);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting payement key");
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
    [Route("Paid")]
    public async Task<IActionResult> GetPaid(string link)
    {
        try
        {
            return await _club.GetPaid(link);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting paid");
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

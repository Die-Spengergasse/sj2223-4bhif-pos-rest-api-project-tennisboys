using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Spg.TennisBooking.Domain.Dtos.ClubDtos;
using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Api.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
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
    //GetAll
    //Post
    //Patch
    //Delete
    //PayementKey
    //Paid //as long it is over a month till end it counts as paid

    [HttpGet("{link}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(string link)
    {
        try
        {
            return await _club.Get(link, Controller.GetUserId(User));
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

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(string? search)
    {
        try
        {
            return await _club.GetAll(search);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting all clubs");
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
    public async Task<IActionResult> Create([FromBody] string name)
    {
        try
        {
            return await _club.Create(name, Controller.GetUserId(User));
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

    [HttpPut]
    //[Authorize]
    public async Task<IActionResult> Put([FromBody] PutClubDto clubDto)
    {
        try
        {
            return await _club.Put(clubDto, Controller.GetUserId(User));
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

    [HttpDelete("{link}")]
    //[Authorize]
    public async Task<IActionResult> Delete(string link)
    {
        try
        {
            return await _club.Delete(link, Controller.GetUserId(User));
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
    [Route("{link}/PayementKey")]
    public async Task<IActionResult> GetPayementKey(string link)
    {
        try
        {
            return await _club.GetPayementKey(link, Controller.GetUserId(User));
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
    [Route("{link}/IsPaid")]
    public async Task<IActionResult> IsPaid(string link)
    {
        try
        {
            return await _club.IsPaid(link, Controller.GetUserId(User));
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

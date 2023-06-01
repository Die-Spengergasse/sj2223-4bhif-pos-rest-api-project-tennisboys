using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.ClubNewsDtos;
using Spg.TennisBooking.Domain.Dtos.UserDtos;
using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Api.Controllers.v2;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
[Authorize]
public class ClubNewsController : ControllerBase
{
    /// <summary>
    /// This APIController is used to do any related ClubNews operations
    /// </summary>
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ClubNewsController> _logger;
    private readonly IClubNewsService _clubNews;

    public ClubNewsController(IWebHostEnvironment env, IConfiguration configuration, ILogger<ClubNewsController> logger, IClubNewsService clubNews)
    {
        _env = env;
        _configuration = configuration;
        _logger = logger;
        _clubNews = clubNews;
    }

    /* ClubNews:
     * ClubEvent and ClubNews essentielly work the same but the both give different context.
     * Get(id)
     * GetAll(Club Link)
     * Post
     * Patch(id)
     * Delete(id)
     */

    /// <summary>
    /// Gets the News of a Club.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>return await _clubNews.Get(id);</returns>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            return await _clubNews.Get(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting news");
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
    /// Gets all News of a Club.
    /// </summary>
    /// <param name="clubLink"></param>
    /// <returns>return await _clubNews.GetAll(clubLink);</returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(string clubLink)
    {
        try
        {
            return await _clubNews.GetAll(clubLink);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting news");
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
    /// Post ClubNews.
    /// </summary>
    /// <param name="postClubNewsDto"></param>
    /// <returns>return await _clubNews.Post(postClubNewsDto, Controller.GetUserId(User));</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostClubNewsDto postClubNewsDto)
    {
        try
        {
            return await _clubNews.Post(postClubNewsDto, Controller.GetUserId(User));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while creating news");
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
    /// Put ClubNews
    /// </summary>
    /// <param name="putClubNewsDto"></param>
    /// <returns>return await _clubNews.Put(putClubNewsDto, Controller.GetUserId(User));</returns>
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] PutClubNewsDto putClubNewsDto)
    {
        try
        {
            return await _clubNews.Put(putClubNewsDto, Controller.GetUserId(User));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while putting news");
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
    /// Delete ClubNews
    /// </summary>
    /// <param name="id"></param>
    /// <returns>return await _clubNews.Delete(id, Controller.GetUserId(User));</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            return await _clubNews.Delete(id, Controller.GetUserId(User));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting news");
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

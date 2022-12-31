using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Spg.TennisBooking.Api.Dtos.UserDtos;
using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
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
}

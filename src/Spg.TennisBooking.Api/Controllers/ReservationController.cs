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
public class ReservationController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ReservationController> _logger;
    private readonly IClubService _club;

    public ReservationController(IWebHostEnvironment env, IConfiguration configuration, ILogger<ReservationController> logger, IClubService club)
    {
        _env = env;
        _configuration = configuration;
        _logger = logger;
        _club = club;
    }
}

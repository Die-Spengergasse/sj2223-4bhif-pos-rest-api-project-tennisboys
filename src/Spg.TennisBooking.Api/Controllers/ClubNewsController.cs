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
public class ClubNewsController : ControllerBase
{
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
}

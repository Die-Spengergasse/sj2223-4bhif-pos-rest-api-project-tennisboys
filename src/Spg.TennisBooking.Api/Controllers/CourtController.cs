using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Spg.TennisBooking.Domain.Dtos.UserDtos;
using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
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
}

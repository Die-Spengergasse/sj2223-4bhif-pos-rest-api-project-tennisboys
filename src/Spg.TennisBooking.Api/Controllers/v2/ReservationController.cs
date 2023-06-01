using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.ReservationDtos;
using Spg.TennisBooking.Domain.Dtos.UserDtos;
using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Api.Controllers.v2;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
[Authorize]
public class ReservationController : ControllerBase
{
    /// <summary>
    /// This APIController is used to do any related Reservation operations
    /// </summary>
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ReservationController> _logger;
    private readonly IReservationService _reservation;

    public ReservationController(IWebHostEnvironment env, IConfiguration configuration, ILogger<ReservationController> logger, IReservationService reservation)
    {
        _env = env;
        _configuration = configuration;
        _logger = logger;
        _reservation = reservation;
    }

    /*
           GetReservationById - Admin
           GetReservationsByClub - Admin
           GetReservationsByCourtId - Only When (Dto as Reponse)
           GetReservationsByUser - Only Themself
           PostReservation
           DeleteReservation - Admin
         */

    /// <summary>
    /// Outputs the UUID of a User
    /// </summary>
    /// <param name="uuid"></param>
    /// <returns>return await _reservation.GetByUUID(uuid, Controller.GetUserId(User));</returns>
    [HttpGet("{uuid}")]
    public async Task<IActionResult> Get(string uuid)
    {
        try
        {
            return await _reservation.GetByUUID(uuid, Controller.GetUserId(User));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting reservation");
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

    //[HttpGet]
    //public async Task<IActionResult> GetAll([FromQuery] string? clubLink, [FromQuery] int? courtId)
    //{
    //    try
    //    {
    //        if(clubLink!=null)
    //        {
    //            _logger.LogInformation("Getting all reservations for club {clubLink}", clubLink);
    //            return await _reservation.GetByClub(clubLink, Controller.GetUserId(User));
    //        }
    //        else if (courtId != null)
    //        {
    //            _logger.LogInformation("Getting all reservations for court {courtId}", courtId);
    //            return await _reservation.GetByCourt((int)courtId);
    //        }
    //        else
    //        {
    //            _logger.LogInformation("Getting all reservations for user {userId}", Controller.GetUserId(User));
    //            return await _reservation.GetByUser(Controller.GetUserId(User));
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        _logger.LogError(e, "Error while getting reservations");
    //        if (_env.IsDevelopment())
    //        {
    //            return StatusCode(500, e.Message);
    //        }
    //        else
    //        {
    //            return StatusCode(500, "Internal Server Error");
    //        }
    //    }
    //}

    /// <summary>
    /// Outputs the Club of the Reservation.
    /// </summary>
    /// <param name="clubLink"></param>
    /// <returns>return await _reservation.GetByClub(clubLink, Controller.GetUserId(User));</returns>
    [HttpGet("club/{clubLink}")]
    public async Task<IActionResult> GetByClub(string clubLink)
    {
        try
        {
            return await _reservation.GetByClub(clubLink, Controller.GetUserId(User));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting reservations");
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
    /// Outputs the CourtId of the Reservation.
    /// </summary>
    /// <param name="courtId"></param>
    /// <returns>return await _reservation.GetByCourt(courtId);</returns>
    [HttpGet("court/{courtId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByCourt(int courtId)
    {
        try
        {
            return await _reservation.GetByCourt(courtId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting reservations");
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
    /// Outputs the User who made the Reservation.
    /// </summary>
    /// <returns>return await _reservation.GetByUser(Controller.GetUserId(User));</returns>
    [HttpGet]
    public async Task<IActionResult> GetByUser()
    {
        try
        {
            return await _reservation.GetByUser(Controller.GetUserId(User));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting reservations");
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
    /// Post a Reservation
    /// </summary>
    /// <param name="reservationDto"></param>
    /// <returns>return await _reservation.Post(reservationDto, Controller.GetUserId(User));</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostReservationDto reservationDto)
    {
        try
        {
            return await _reservation.Post(reservationDto, Controller.GetUserId(User));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while posting reservation");
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
    /// Deletes a Reservation.
    /// </summary>
    /// <param name="uuid"></param>
    /// <returns>return await _reservation.Delete(uuid, Controller.GetUserId(User));</returns>
    [HttpDelete("{uuid}")]
    public async Task<IActionResult> Delete(string uuid)
    {
        try
        {
            return await _reservation.Delete(uuid, Controller.GetUserId(User));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting reservation");
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

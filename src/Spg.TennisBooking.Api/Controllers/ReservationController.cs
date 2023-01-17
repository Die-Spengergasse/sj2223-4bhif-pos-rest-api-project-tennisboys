using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.ReservationDtos;
using Spg.TennisBooking.Domain.Dtos.UserDtos;
using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReservationController : ControllerBase
{
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

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? clubLink, [FromQuery] int? courtId)
    {
        try
        {
            if(clubLink!=null)
            {
                return await _reservation.GetByClub(clubLink, Controller.GetUserId(User));
            }
            else if (courtId != null)
            {
                return await _reservation.GetByCourt((int)courtId);
            }
            else
            {
                return await _reservation.GetByUser(Controller.GetUserId(User));
            }
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
    
    //[HttpGet]
    //public async Task<IActionResult> GetByClub([FromQuery] string clubLink)
    //{
    //    try
    //    {
    //        return await _reservation.GetByClub(clubLink, Controller.GetUserId(User));
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

    //[HttpGet]
    //public async Task<IActionResult> GetByCourt([FromQuery] int courtId)
    //{
    //    try
    //    {
    //        return await _reservation.GetByCourt(courtId);
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

    //[HttpGet]
    //public async Task<IActionResult> GetByUser()
    //{
    //    try
    //    {
    //        return await _reservation.GetByUser(Controller.GetUserId(User));
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

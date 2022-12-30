﻿using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Spg.TennisBooking.Api.Dtos.UserDtos;
using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Api.Controllers
{
    /// <summary>
    /// This APIController is used to do any related Account operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;        
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _user;
        
        public UserController(IWebHostEnvironment env, IConfiguration configuration, ILogger<UserController> logger, IUserService user)
        {
            _env = env;
            _configuration = configuration;
            _logger = logger;
            _user = user;
        }

        //Welcomed
        [HttpGet]
        [Route("welcomed")]
        [Authorize]
        public IActionResult Welcomed()
        {
            _logger.LogInformation("Welcomed");
            string? uuid = User.FindFirst(ClaimTypes.Name)?.Value;
            if (uuid == null) return BadRequest("No UUID found");
            _logger.LogInformation("UUID: {uuid}", uuid);
            try
            {
                return Ok(_user.Welcomed(uuid));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while checking if user is welcomed");
                if (e is HttpException exception)
                {
                    return new ObjectResult(new { message = e.Message }) { StatusCode = (int?)exception.StatusCode };
                }
                else
                {
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

        //PersonalData
        [HttpPatch("PersonalData")]
        [Produces("application/json")]
        [Authorize]
        public IActionResult PersonalData([FromBody] PersonalDataDto personalDataDto)
        {
            _logger.LogInformation("PersonalData");
            //Give out every information about the user
            string? uuid = User.FindFirst(ClaimTypes.Name)?.Value;
            if (uuid == null) return BadRequest("No UUID found");
            _logger.LogInformation("UserId: {UUID}", uuid);
            try
            {
                bool success = _user.PersonalData(uuid, personalDataDto.FirstName, personalDataDto.LastName, personalDataDto.BirthDate, personalDataDto.Gender, personalDataDto.PhoneNumber);
                _logger.LogInformation("PersonalData: {success}. UUID: {UUID}, FirstName: {FirstName}, LastName: {LastName}, BirthDate: {BirthDate}, Gender: {Gender}, PhoneNumber: {PhoneNumber}", success, uuid, personalDataDto.FirstName, personalDataDto.LastName, personalDataDto.BirthDate, personalDataDto.Gender, personalDataDto.PhoneNumber);
                return new ObjectResult(new { }) { StatusCode = (int)HttpStatusCode.OK };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "PersonalData: {UUID}, FirstName: {FirstName}, LastName: {LastName}, BirthDate: {BirthDate}, Gender: {Gender}, PhoneNumber: {PhoneNumber}", uuid, personalDataDto.FirstName, personalDataDto.LastName, personalDataDto.BirthDate, personalDataDto.Gender, personalDataDto.PhoneNumber);
                if (e is HttpException exception)
                {
                    return new ObjectResult(new { message = e.Message }) { StatusCode = (int?)exception.StatusCode };
                }
                else
                {
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

        //ChangePassword
        [HttpPatch("ChangePassword")]
        [Produces("application/json")]
        [Authorize]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            _logger.LogInformation("ChangePassword");
            //Change the password of the user
            string? uuid = User.FindFirst(ClaimTypes.Name)?.Value;
            if (uuid == null) return BadRequest("No UUID found");
            _logger.LogInformation("UserId: {UUID}", uuid);
            try
            {
                bool success = _user.ChangePassword(uuid, changePasswordDto.Password, changePasswordDto.NewPassword);
                _logger.LogInformation("ChangePassword: {success}. UUID: {UUID}, Password: {Password}, NewPassword: {NewPassword}", success, uuid, changePasswordDto.Password, changePasswordDto.NewPassword);
                return new ObjectResult(new { }) { StatusCode = (int)HttpStatusCode.OK };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ChangePassword: {UUID}, Password: {Password}, NewPassword: {NewPassword}", uuid, changePasswordDto.Password, changePasswordDto.NewPassword);
                if (e is HttpException exception)
                {
                    return new ObjectResult(new { message = e.Message }) { StatusCode = (int?)exception.StatusCode };
                }
                else
                {
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
    }
}
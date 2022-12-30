using System.Net;

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
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;        
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _user;

        public UserController(IWebHostEnvironment env, IConfiguration configuration, ILogger<AuthController> logger, IUserService user)
        {
            _env = env;
            _configuration = configuration;
            _logger = logger;
            _user = user;
        }
        
        //PersonalData
        [HttpGet("PersonalData")]
        [Produces("application/json")]
        public IActionResult PersonalData([FromBody] PersonalDataDto personalDataDto)
        {
            try
            {
                bool success = _user.PersonalData(personalDataDto.UUID, personalDataDto.FirstName, personalDataDto.LastName, personalDataDto.BirthDate, personalDataDto.Gender, personalDataDto.PhoneNumber);
                //_logger.LogInformation("EmailInUse: {email}: {emailInUse}", email, emailInUse);
                return new ObjectResult(new { }) { StatusCode = (int)HttpStatusCode.OK };
            }
            catch (Exception e)
            {
                //_logger.LogError(e, "EmailInUse: {email}", email);
                if (e is HttpException)
                {
                    return new ObjectResult(new { message = e.Message }) { StatusCode = (int?)((HttpException)e).StatusCode };
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
        [HttpGet("ChangePassword")]
        [Produces("application/json")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            try
            {
                bool success = _user.ChangePassword(changePasswordDto.UUID, changePasswordDto.Password, changePasswordDto.NewPassword);
                //_logger.LogInformation("EmailInUse: {email}: {emailInUse}", email, emailInUse);
                return new ObjectResult(new { }) { StatusCode = (int)HttpStatusCode.OK };
            }
            catch (Exception e)
            {
                //_logger.LogError(e, "EmailInUse: {email}", email);
                if (e is HttpException)
                {
                    return new ObjectResult(new { message = e.Message }) { StatusCode = (int?)((HttpException)e).StatusCode };
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
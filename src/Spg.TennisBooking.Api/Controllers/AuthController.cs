using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Spg.TennisBooking.Infrastructure;
using Spg.TennisBooking.Domain.Model;
using Microsoft.AspNetCore.Authorization;
// using Spg.TennisBooking.Dto.Models;
// using Spg.TennisBooking.Configuration.Model;
using Spg.TennisBooking.Application;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Application.Services;
using Spg.TennisBooking.Api.Dtos.AuthDtos;
using System.Net;
using Spg.TennisBooking.Domain.Exceptions;

namespace Spg.TennisBooking.Api.Controllers
{
    /// <summary>
    /// This APIController is used to do any related Account operations
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _auth;

        public AuthController(IConfiguration IConfiguration, IAuthService auth)
        {
            _configuration = IConfiguration;
            _auth = auth;
        }

        [HttpGet("EmailInUse")]
        [Produces("application/json")]
        public IActionResult EmailInUse(string email)
        {
            try
            {
                bool emailInUse = _auth.EmailInUse(email);
                return Ok(emailInUse);
            }
            catch (HttpException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }

        [HttpPost("Register")]
        [Produces("application/json")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                User user = _auth.Register(registerDto.Email, registerDto.Password);
                //Return 201 Created and the location of the new resource
                string url = _configuration.GetSection("MvcFrontEnd").Value;
                Uri uri = new Uri(url + "/verify?uuid=" + user.UUID);
                return Created(uri.AbsoluteUri, new { user = user.UUID });
            }
            catch (HttpException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }

        [HttpPost("Verify")]
        [Produces("application/json")]
        public IActionResult Verify([FromBody] VerifyDto verifyDto)
        {
            try
            {
                bool verified = _auth.Verify(verifyDto.UUID, verifyDto.VerificationCode);
                //Return Verification Success and link to login
                string url = _configuration.GetSection("MvcFrontEnd").Value;
                Uri uri = new Uri(url + "/login");
                return Created(uri.AbsolutePath, new { verified = verified });
            }
            catch (HttpException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }

        [HttpPost("Login")]
        [Produces("application/json")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            try
            {
                string token = _auth.Login(loginDto.Email, loginDto.Password, _configuration.GetSection("jwtSecret").Value);
                return Ok(new { token = token });
            }
            catch (HttpException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }

        //Forgot Password
        [HttpPost("ForgotPassword")]
        [Produces("application/json")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            try
            {
                bool success = _auth.ForgotPassword(forgotPasswordDto.Email);
                return Ok(new { success = success });
            }
            catch (HttpException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }

        //Reset Password
        [HttpPost("ResetPassword")]
        [Produces("application/json")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            try
            {
                bool success = _auth.ResetPassword(resetPasswordDto.Email, resetPasswordDto.Password, resetPasswordDto.ResetCode);
                return Ok(new { success = success });
            }
            catch (HttpException e)
            {
                return StatusCode((int)e.StatusCode, e.Message);
            }
        }
    }
}
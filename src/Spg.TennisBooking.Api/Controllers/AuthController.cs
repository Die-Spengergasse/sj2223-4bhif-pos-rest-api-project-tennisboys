using System.Net;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Spg.TennisBooking.Api.Dtos.AuthDtos;
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
    public class AuthController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;        
        private readonly IConfiguration _configuration;
        private readonly IAuthService _auth;

        public AuthController(IWebHostEnvironment env, IConfiguration IConfiguration, IAuthService auth)
        {
            _env = env;
            _configuration = IConfiguration;
            _auth = auth;
        }
        
        //EmailInUse
        [HttpGet("EmailInUse")]
        [Produces("application/json")]
        public IActionResult EmailInUse(string email)
        {
            try
            {
                bool emailInUse = _auth.EmailInUse(email);
                return new ObjectResult(new { emailInUse = emailInUse }) { StatusCode = (int)HttpStatusCode.OK };
            }
            catch (Exception e)
            {
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
        
        //Register
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
                return Created(uri.AbsoluteUri, new { uuid = user.UUID });
            }
            catch (Exception e)
            {
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
        
        //Verify
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
            catch (Exception e)
            {
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
        
        //Login
        [HttpPost("Login")]
        [Produces("application/json")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            try
            {
                string token = _auth.Login(loginDto.Email, loginDto.Password, _configuration.GetSection("jwtSecret").Value);
                //Return Token and link to UserPage
                string url = _configuration.GetSection("MvcFrontEnd").Value;
                Uri uri = new Uri(url + "/user");
                return Created(uri.AbsolutePath, new { token = token });
            }
            catch (Exception e)
            {
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

        //Forgot Password
        [HttpPost("ForgotPassword")]
        [Produces("application/json")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            try
            {
                User user = _auth.ForgotPassword(forgotPasswordDto.Email);
                //Return uuid and link to reset password
                string url = _configuration.GetSection("MvcFrontEnd").Value;
                Uri uri = new Uri(url + "/resetpassword" + "?uuid=" + user.UUID);
                return Created(uri.AbsolutePath, new { uuid = user.UUID });
            }
            catch (Exception e)
            {
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

        //Reset Password
        [HttpPost("ResetPassword")]
        [Produces("application/json")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            try
            {
                bool success = _auth.ResetPassword(resetPasswordDto.UUID, resetPasswordDto.Password, resetPasswordDto.ResetCode);
                //Return success and link to login
                string url = _configuration.GetSection("MvcFrontEnd").Value;
                Uri uri = new Uri(url + "/login");
                return Created(uri.AbsolutePath, new { success = success });
            }
            catch (Exception e)
            {
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
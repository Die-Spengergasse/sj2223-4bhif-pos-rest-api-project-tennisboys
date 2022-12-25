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
using Spg.TennisBooking.Api.Dtos;

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
        private readonly IAuthService _auth;
        
        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        public string Login([FromBody] LoginDto loginDto)
        {
            return _auth.Login(loginDto.Username, loginDto.Password);
        }

        [HttpPost("register")]
        public bool Register([FromBody] LoginDto loginDto)
        {
            return _auth.Register(loginDto.Username, loginDto.Password);
        }
    }
}
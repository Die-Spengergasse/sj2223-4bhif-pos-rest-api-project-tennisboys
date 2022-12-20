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
using ZID.Automat.Api.Dtos;

namespace ZID.Automat.Api.Controllers
{
    /// <summary>
    /// This APIController is used to do any related Account operations
    /// </summary>
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly IControllerAuthService _controllerAuthService;
        public AuthenticationController(IAuthService auth, IControllerAuthService controllerAuthService)
        {
            _auth = auth;
            _controllerAuthService = controllerAuthService;
        }

        [HttpPost("login")]
        public string? Login([FromBody] LoginDto loginDto)
        {
            return _auth.Auth(loginDto);
        }
    }
}
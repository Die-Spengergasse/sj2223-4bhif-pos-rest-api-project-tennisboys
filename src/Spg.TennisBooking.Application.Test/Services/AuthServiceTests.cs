using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;
using Spg.TennisBooking.Application.Services;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Xunit;

namespace Spg.TennisBooking.Application.Services
{
    public class AuthServiceTests
    {
        private readonly IAuthService _authService;

        public AuthServiceTests(IAuthService authService)
        {
            _authService = authService;
        }

        //EmailInUse
        [Fact]
        public void EmailInUse()
        {
        }

        //Register
        [Fact]
        public void Register()
        {

        }

        //Verify
        [Fact]
        public void Verify()
        {
            
        }

        //Login
        [Fact]
        public void Login()
        {
            
        }

        //ForgotPassword
        [Fact]
        public void ForgotPassword()
        {
            
        }

        //ResetPassword
        [Fact]
        public void ResetPassword()
        {

        }
    }
}

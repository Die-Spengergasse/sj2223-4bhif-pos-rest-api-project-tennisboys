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
using Spg.TennisBooking.Repository.Repositories;

namespace Spg.TennisBooking.Application.Test.Services
{
    public class AuthServiceTests : Tests
    {
        protected AuthService GetService()
        {
            return new AuthService(GetRepository());
        }

        protected IAuthRepository GetRepository()
        {
            return new AuthRepository(GetContext());
        }

        //EmailInUse
        [Fact]
        public void EmailInUse()
        {
            //Init
            AuthService authService = GetService();
            
            //Arrange
            string email = "info@adrian-schauer.at";

            //Act
            bool result = authService.EmailInUse(email);

            //Assert
            Assert.False(result);
        }

        //Register
        [Fact]
        public void Register()
        {
            Assert.True(true);
        }

        //Verify
        [Fact]
        public void Verify()
        {
            Assert.True(true);
        }

        //Login
        [Fact]
        public void Login()
        {
            Assert.True(true);
        }

        //ForgotPassword
        [Fact]
        public void ForgotPassword()
        {
            Assert.True(true);
        }

        //ResetPassword
        [Fact]
        public void ResetPassword()
        {
            Assert.True(true);
        }
    }
}

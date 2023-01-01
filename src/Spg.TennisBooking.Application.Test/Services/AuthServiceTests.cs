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
        protected AuthService GetAuthService()
        {
            return new AuthService(GetAuthRepository());
        }

        protected IAuthRepository GetAuthRepository()
        {
            return new AuthRepository(GetContext());
        }

        //EmailInUse
        [Fact]
        public void EmailInUse()
        {
            //Init
            AuthService authService = GetAuthService();
            
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

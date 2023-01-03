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
            //Init
            AuthService authService = GetService();

            //Arrange
            string email = "info@adrian-schauer.at";
            string password = "admin";

            //Act
            User result = authService.Register(email, password);

            //Assert
            Assert.NotNull(result);
        }


        //Verify
        [Fact]
        public void Verify()
        {
            //Init
            AuthService authService = GetService();

            //Arrange
            string uuid = "d4f5d4f5d4f5d4f5d4f5d4f5d4f5d4f5";
            string verificationCode = "d4f5d4f5d4f5d4f5d4f5d4f5d4f5d4f5";

            //Act
            bool result = authService.Verify(uuid, verificationCode);

            //Assert
            Assert.True(result);
        }

        //Login
        [Fact]
        public void Login()
        {
            //Init
            AuthService authService = GetService();

            //Arrange
            string email = "info@adrian-schauer.at";
            string password = "admin";
            string secret = "1234";

            //Act
            bool result = authService.Login(email, password, secret);

            //Assert
            Assert.True(result);
        }

        //ForgotPassword
        [Fact]
        public void ForgotPassword()
        {
            //Init
            AuthService authService = GetService();

            //Arrange
            string email = "info@adrian-schauer.at";

            //Act
            User result = authService.ForgotPassword(email);

            //Assert
            Assert.NotNull(result);
        }

        //ResetPassword
        [Fact]
        public void ResetPassword()
        {
            //Init
            AuthService authService = GetService();

            //Arrange
            string uuid = "d4f5d4f5d4f5d4f5d4f5d4f5d4f5d4f5";
            string password = "admin";
            string resetCode = "hkjsfdio890";

            //Act
            bool result = authService.ResetPassword(uuid, password, resetCode);

            //Assert
            Assert.True(result);
        }
       
    }
}

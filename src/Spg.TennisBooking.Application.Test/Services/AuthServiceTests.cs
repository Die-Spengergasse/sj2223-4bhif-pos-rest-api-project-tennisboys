using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;
using Spg.TennisBooking.Application.Services;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Xunit;
using Spg.TennisBooking.Repository.Repositories;
using Spg.TennisBooking.Infrastructure;

namespace Spg.TennisBooking.Application.Test.Services
{
    public class AuthServiceTests : Tests
    {
        protected AuthService GetService(IUserRepository userRepository)
        {
            return GetAuthService(userRepository);
        }

        protected UserRepository GetRepository(TennisBookingContext context)
        {
            return GetUserRepository(context);
        }

        //EmailInUse
        [Fact]
        public void EmailInUse()
        {
            //Init
            TennisBookingContext context = GetContext();
            UserRepository userRepository = GetRepository(context);
            AuthService authService = GetService(userRepository);
            
            //Arrange
            string email = "info@adrian-schauer.at";

            //Act
            bool result = authService.EmailInUse(email);

            //Assert
            Assert.False(result);

            //Clean
            context.Database.EnsureDeleted();
        }

        //Register
        [Fact]
        public void Register()
        {
            //Init
            TennisBookingContext context = GetContext();
            UserRepository userRepository = GetRepository(context);
            AuthService userService = GetService(userRepository);

            //Arrange
            string email = "info@adrian-schauer.at";
            string password = "admin1234";

            //Act
            User result = userService.Register(email, password);

            //Assert
            Assert.NotNull(result);
            
            //Clean
            context.Database.EnsureDeleted();
        }


        //Verify
        [Fact]
        public void Verify()
        {
            //Init
            TennisBookingContext context = GetContext();
            UserRepository userRepository = GetRepository(context);
            AuthService userService = GetService(userRepository);

            //Arrange
            string email = "info@adrian-schauer.at";
            string password = "admin1234";

            //Register
            User user = userService.Register(email, password);

            //Act
            bool result = userService.Verify(user.UUID, user.VerificationCode);

            //Assert
            Assert.True(result);
            
            //Clean
            context.Database.EnsureDeleted();
        }

        //Login
        [Fact]
        public void Login()
        {
            //Init
            TennisBookingContext context = GetContext();
            UserRepository userRepository = GetRepository(context);
            AuthService userService = GetService(userRepository);

            //Arrange
            string email = "info@adrian-schauer.at";
            string password = "admin1234";

            //Register
            User user = userService.Register(email, password);

            //Verify
            userService.Verify(user.UUID, user.VerificationCode);

            //Act
            string result = userService.Login(email, password, "This is only a Test Key. Do not use in prod!");

            //Assert
            Assert.True(!string.IsNullOrEmpty(result));
            
            //Clean
            context.Database.EnsureDeleted();
        }

        //ForgotPassword
        [Fact]
        public void ForgotPassword()
        {
            //Init
            TennisBookingContext context = GetContext();
            UserRepository userRepository = GetRepository(context);
            AuthService authService = GetService(userRepository);

            //Arrange
            string email = "info@adrian-schauer.at";
            string password = "admin1234";

            //Register
            User user = authService.Register(email, password);

            //Verify
            authService.Verify(user.UUID, user.VerificationCode);

            //Act
            User result = authService.ForgotPassword(email);

            //Assert
            Assert.NotNull(result);
            
            //Clean
            context.Database.EnsureDeleted();
        }

        //ResetPassword
        [Fact]
        public void ResetPassword()
        {
            //Init
            TennisBookingContext context = GetContext();
            UserRepository userRepository = GetRepository(context);
            AuthService userService = GetService(userRepository);

            //Arrange
            string email = "info@adrian-schauer.at";
            string password = "admin1234";

            //Register
            User user = userService.Register(email, password);

            //Verify
            userService.Verify(user.UUID, user.VerificationCode);

            //For
            user = userService.ForgotPassword(email);

            //Act
            bool result = userService.ResetPassword(user.UUID, password, user.ResetCode);

            //Assert
            Assert.True(result);
            
            //Clean
            context.Database.EnsureDeleted();
        }
    }
}
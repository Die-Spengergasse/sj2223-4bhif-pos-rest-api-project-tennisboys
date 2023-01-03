using Spg.TennisBooking.Application.Services;
using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;
using Spg.TennisBooking.Infrastructure;
using Spg.TennisBooking.Repository.Repositories;
using System.Net;
using System.Security.Cryptography;
using Xunit;

namespace Spg.TennisBooking.Application.Test.Services
{
    public class UserServiceTests : Tests
    {
        protected UserService GetService(IUserRepository userRepository)
        {
            return new UserService(userRepository);
        }

        protected IUserRepository GetRepository(TennisBookingContext context)
        {
            return new UserRepository(context);
        }

        //Welcomed
        [Fact]
        public void Welcomed()
        {
            TennisBookingContext context = GetContext();
            UserService userService = GetService(GetRepository(context));
            AuthService authService = GetAuthService(GetAuthRepository(context));

            //Arrange
            string email = "info@adrian-schauer.at";
            string password = "admin1234";

            //Register
            User user = authService.Register(email, password);

            //Verify
            authService.Verify(user.UUID, user.VerificationCode);

            //Act
            bool result = userService.Welcomed(user.UUID);

            Assert.False(result);
        }

        //GetPersonalData
        [Fact]
        public void GetPersonalData()
        {
            Assert.True(true);
        }

        //SetPersonalData
        [Fact]
        public void SetPersonalData()
        {
            Assert.True(true);
        }

        //ChangePassword
        [Fact]
        public void ChangePassword()
        {
            Assert.True(true);
        }
    }
}

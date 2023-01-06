using Spg.TennisBooking.Application.Services;
using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;
using Spg.TennisBooking.Infrastructure;
using Spg.TennisBooking.Repository.Repositories;
using System;
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

            Assert.True(result);
        }

        //GetPersonalData
        [Fact]
        public void GetPersonalData()
        {
            //Init
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
            User result = userService.GetPersonalData(user.UUID);

            //Assert
            Assert.True(true);

            //Clean
            context.Database.EnsureDeleted();

            
        }

        //SetPersonalData
        [Fact]
        public void SetPersonalData()
        {
            //Init
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
            bool result = userService.SetPersonalData(user.UUID, user.FirstName, user.LastName, user.BirthDate, user.Gender, user.PhoneNumber);

            //Assert
            Assert.True(result);

            //Clean
            context.Database.EnsureDeleted();
        }

        //ChangePassword
        [Fact]
        public void ChangePassword()
        {
            //Init
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
            bool result = userService.ChangePassword(user.UUID, password, password);

            //Assert
            Assert.True(true);
            
            //Clean
            context.Database.EnsureDeleted();
        }
    }
}

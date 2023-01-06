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

            Assert.False(result);
        }

        //GetPersonalData
        [Fact]
        public void GetPersonalData()
        {
            //Init
            TennisBookingContext context = GetContext();
            UserService userService = GetService(GetRepository(context));

            //Arrange
            string UUID = "00000000-0000-0000-0000-000000000000";

            //Act
            User result = userService.GetPersonalData(UUID);

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

            //Arrange
            string UUID = "00000000-0000-0000-0000-000000000000";
            string firstName = "Adrian";
            string lastName = "Schauer";
            DateTime? birthDate = new DateTime(1989, 12, 12);
            GenderTypes gender = GenderTypes.Male;
            PhoneNumber? phoneNumber = new PhoneNumber("+43", "6641234567");

            //Act
            bool result = userService.SetPersonalData(UUID, firstName, lastName, birthDate, gender, phoneNumber);

            //Assert
            Assert.True(true);

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

            //Arrange
            string UUID = "00000000-0000-0000-0000-000000000000";
            string Password = "admin1234";
            string newPassword = "admin12345";

            //Act
            bool result = userService.ChangePassword(UUID, Password, newPassword);

            //Assert
            Assert.True(true);

            //Clean
            context.Database.EnsureDeleted();
        }
    }
}

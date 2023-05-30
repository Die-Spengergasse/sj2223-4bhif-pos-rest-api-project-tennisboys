using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Spg.TennisBooking.Application.Services.v2;
using Spg.TennisBooking.Domain.Dtos.ClubDtos;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;
using Spg.TennisBooking.Infrastructure.v2;
using Spg.TennisBooking.Repository.Repositories.v2;
using Xunit;

namespace Spg.TennisBooking.Application.Test.Services.v2
{
    public class ClubServiceTests : Tests
    {
        /*
        Create
        Delete
        Get
        GetAll
        GetPayementKey
        IsPaid
        Patch
        */
        [Fact]
        public async void Create(){
            TennisBookingContext context = GetContext();
            UserRepository userRepository = GetUserRepository(context);
            AuthService authService = GetAuthService(userRepository);
            ILogger<ClubService> logger = new Logger<ClubService>(new LoggerFactory());
            SocialHubRepository socialHubRepository = GetSocialHubRepository(context);
            ClubRepository clubRepository = GetClubRepository(context);
            ClubService clubService = GetClubService(clubRepository, userRepository, logger, socialHubRepository);
            // Arrange
            string email = "info@adrian-schauer.at";
            string password = "admin1234";

             //Register
            User user = authService.Register(email, password);

            //Verify
            authService.Verify(user.UUID, user.VerificationCode);
            
            //Act
            IActionResult result = await clubService.Create("TC Eichgraben", user.UUID);

            //Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async void Delete(){
            TennisBookingContext context = GetContext();
            UserRepository userRepository = GetUserRepository(context);
            AuthService authService = GetAuthService(userRepository);
            ILogger<ClubService> logger = new Logger<ClubService>(new LoggerFactory());
            ClubRepository clubRepository = GetClubRepository(context);
            SocialHubRepository socialHubRepository = GetSocialHubRepository(context);
            ClubService clubService = GetClubService(clubRepository, userRepository, logger, socialHubRepository);

            // Arrange
            string email = "info@adrian-schauer.at";
            string password = "admin1234";

             //Register
            User user = authService.Register(email, password);

            //Verify
            authService.Verify(user.UUID, user.VerificationCode);

            //Create
            IActionResult created = await clubService.Create("TC Eichgraben", user.UUID);

            Club? club = await clubRepository.Get(1);
            Assert.NotNull(club);

            //Act
            IActionResult result = await clubService.Delete(club.Link, user.UUID);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Get(){
            TennisBookingContext context = GetContext();
            UserRepository userRepository = GetUserRepository(context);
            AuthService authService = GetAuthService(userRepository);
            ILogger<ClubService> logger = new Logger<ClubService>(new LoggerFactory());
            ClubRepository clubRepository = GetClubRepository(context);
            SocialHubRepository socialHubRepository = GetSocialHubRepository(context);
            ClubService clubService = GetClubService(clubRepository, userRepository, logger, socialHubRepository);
            // Arrange
            string email = "adrian@schauer.at";
            string password = "admin1234";

             //Register
            User user = authService.Register(email, password);

            //Verify
            authService.Verify(user.UUID, user.VerificationCode);

            //Create
            IActionResult created = await clubService.Create("TC Eichgraben", user.UUID);

            Club? club = await clubRepository.Get(1);
            Assert.NotNull(club);

            //Act
            IActionResult result = await clubService.Get(club.Link, user.UUID);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetAll(){
            TennisBookingContext context = GetContext();
            UserRepository userRepository = GetUserRepository(context);
            AuthService authService = GetAuthService(userRepository);
            ILogger<ClubService> logger = new Logger<ClubService>(new LoggerFactory());
            ClubRepository clubRepository = GetClubRepository(context);
            SocialHubRepository socialHubRepository = GetSocialHubRepository(context);
            ClubService clubService = GetClubService(clubRepository, userRepository, logger, socialHubRepository);
            
            // Arrange
            string email = "adrian@schauer.at";
            string password = "admin1234";

             //Register
             User user = authService.Register(email, password);

            //Verify
            authService.Verify(user.UUID, user.VerificationCode);

            //Create
            IActionResult created = await clubService.Create("TC Eichgraben", user.UUID);

            Club? club = await clubRepository.Get(1);
            Assert.NotNull(club);

            //Act
            IActionResult result = await clubService.GetAll(user.UUID);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        //TODO: Will be implemented later
        [Fact]
        public async void GetPayementKey(){
            TennisBookingContext context = GetContext();
            UserRepository userRepository = GetUserRepository(context);
            AuthService authService = GetAuthService(userRepository);
            ILogger<ClubService> logger = new Logger<ClubService>(new LoggerFactory());
            ClubRepository clubRepository = GetClubRepository(context);
            SocialHubRepository socialHubRepository = GetSocialHubRepository(context);
            ClubService clubService = GetClubService(clubRepository, userRepository, logger, socialHubRepository);
            // Arrange
            string email = "adrian@schauer.at";
            string password ="admin1234";

             //Register
            User user = authService.Register(email, password);

            //Verify
            authService.Verify(user.UUID, user.VerificationCode);

            //Create
            IActionResult created = await clubService.Create("TC Eichgraben", user.UUID);

            Club? club = await clubRepository.Get(1);
            // Assert.NotNull(club);

            //Act
            // IActionResult result = await clubService.GetPayementKey(club.Link, user.UUID);

            //Assert
            // Assert.IsType<OkObjectResult>(result);

            Assert.True(true);
        }

        [Fact]
        public async void IsPaid(){
            TennisBookingContext context = GetContext();
            UserRepository userRepository = GetUserRepository(context);
            AuthService authService = GetAuthService(userRepository);
            ILogger<ClubService> logger = new Logger<ClubService>(new LoggerFactory());
            ClubRepository clubRepository = GetClubRepository(context);
            SocialHubRepository socialHubRepository = GetSocialHubRepository(context);
            ClubService clubService = GetClubService(clubRepository, userRepository, logger, socialHubRepository);
            // Arrange
            string email = "adrian@schauer.at";
            string password = "admin1234";

             //Register
            User user = authService.Register(email, password);

            //Verify
            authService.Verify(user.UUID, user.VerificationCode);

            //Create
            IActionResult created = await clubService.Create("TC Eichgraben", user.UUID);

            Club? club = await clubRepository.Get(1);
            Assert.NotNull(club);

            //Act
            IActionResult result = await clubService.IsPaid(club.Link, user.UUID);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Put(){
            TennisBookingContext context = GetContext();
            UserRepository userRepository = GetUserRepository(context);
            AuthService authService = GetAuthService(userRepository);
            ILogger<ClubService> logger = new Logger<ClubService>(new LoggerFactory());
            ClubRepository clubRepository = GetClubRepository(context);
            SocialHubRepository socialHubRepository = GetSocialHubRepository(context);
            ClubService clubService = GetClubService(clubRepository, userRepository, logger, socialHubRepository);
            // Arrange
            string email = "adrian@schauer.at";
            string password = "admin1234";

             //Register
            User user = authService.Register(email, password);

            //Verify
            authService.Verify(user.UUID, user.VerificationCode);

            //Create Club
            IActionResult created = await clubService.Create("TC Eichgraben", user.UUID);

            Club? club = await clubRepository.Get(1);
            Assert.NotNull(club);

            //Create Dto
            PutClubDto patchClubDto = new PutClubDto(){
                Link = club.Link,
                Name = "TC Eichgraben",
                Info = "Some Info",
                Address = "Eichgraben 1",
                ZipCode = "Eichgraben",
                ImagePath = "https://www.tc-eichgraben.at",
            };

            //Act
            IActionResult result = await clubService.Put(patchClubDto, user.UUID);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}

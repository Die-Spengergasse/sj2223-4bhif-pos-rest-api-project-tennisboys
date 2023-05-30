using Microsoft.Extensions.Logging;
using Spg.TennisBooking.Application.Services.v1;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Infrastructure.v1;
using Spg.TennisBooking.Repository.Repositories.v1;

namespace Spg.TennisBooking.Application.Test.Services.v1
{
    public class Tests : Spg.TennisBooking.Domain.Test.v1.Tests
    {
        //Services
        protected AuthService GetAuthService(IUserRepository userRepository)
        {
            return new AuthService(userRepository);
        }
        protected UserService GetUserService(IUserRepository userRepository)
        {
            return new UserService(userRepository);
        }
        protected ClubService GetClubService(IClubRepository clubRepository, IUserRepository userRepository, ILogger<ClubService> logger, ISocialHubRepository socialHubRepository)
        {
            return new ClubService(clubRepository, userRepository, socialHubRepository, logger);
        }

        //Repositories
        protected UserRepository GetUserRepository(TennisBookingContext context)
        {
            return new UserRepository(context);
        }
        protected ClubRepository GetClubRepository(TennisBookingContext context)
        {
            return new ClubRepository(context);
        }

        protected SocialHubRepository GetSocialHubRepository(TennisBookingContext context)
        {
            return new SocialHubRepository(context);
        }
    }
}
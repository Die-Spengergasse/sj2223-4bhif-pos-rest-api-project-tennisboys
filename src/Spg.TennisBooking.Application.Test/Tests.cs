using Spg.TennisBooking.Application.Services;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Infrastructure;
using Spg.TennisBooking.Repository.Repositories;

namespace Spg.TennisBooking.Application.Test
{
    public class Tests : Spg.TennisBooking.Domain.Test.Tests
    {
        protected AuthService GetAuthService(IUserRepository userRepository)
        {
            return new AuthService(userRepository);
        }

        protected UserRepository GetUserRepository(TennisBookingContext context)
        {
            return new UserRepository(context);
        }
    }
}
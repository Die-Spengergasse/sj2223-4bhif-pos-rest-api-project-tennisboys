using Spg.TennisBooking.Application.Services;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Infrastructure;
using Spg.TennisBooking.Repository.Repositories;

namespace Spg.TennisBooking.Application.Test
{
    public class Tests : Spg.TennisBooking.Domain.Test.Tests
    {
        protected AuthService GetAuthService(IAuthRepository authRepository)
        {
            return new AuthService(authRepository);
        }

        protected AuthRepository GetAuthRepository(TennisBookingContext context)
        {
            return new AuthRepository(context);
        }
    }
}
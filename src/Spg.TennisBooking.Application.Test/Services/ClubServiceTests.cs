using Spg.TennisBooking.Domain.Interfaces;

namespace Spg.TennisBooking.Application.Test.Services
{
    public class ClubServiceTests
    {
        private readonly IClubService _clubService;

        public ClubServiceTests(IClubService clubService)
        {
            _clubService = clubService;
        }
    }
}

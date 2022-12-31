using Spg.TennisBooking.Domain.Interfaces;

namespace Spg.TennisBooking.Application.Services
{
    public class ClubServiceTests
    {
        private readonly IClubService _clubService;

        public ClubServiceTests(IClubService clubService)
        {
            _clubService = clubService;
        }
}

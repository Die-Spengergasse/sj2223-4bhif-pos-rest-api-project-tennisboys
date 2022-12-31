using Spg.TennisBooking.Domain.Interfaces;

namespace Spg.TennisBooking.Application.Services
{
    public class ClubNewsServiceTests
    {
        private readonly IClubNewsService _clubNewsService;

        public ClubNewsServiceTests(IClubNewsService clubNewsService)
        {
            _clubNewsService = clubNewsService;
        }
    }
}

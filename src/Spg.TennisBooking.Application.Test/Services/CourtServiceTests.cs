using Spg.TennisBooking.Domain.Interfaces;

namespace Spg.TennisBooking.Application.Services
{
    public class CourtServiceTests
    {
        private readonly ICourtService _courtService;

        public CourtServiceTests(ICourtService courtService)
        {
            _courtService = courtService;
        }
    }
}

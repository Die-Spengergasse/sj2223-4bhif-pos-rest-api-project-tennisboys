using Spg.TennisBooking.Domain.Interfaces;

namespace Spg.TennisBooking.Application.Test.Services
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

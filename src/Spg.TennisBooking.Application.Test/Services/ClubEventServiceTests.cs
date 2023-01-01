using Spg.TennisBooking.Domain.Interfaces;

namespace Spg.TennisBooking.Application.Test.Services
{
    public class ClubEventServiceTests
    {
        private readonly IClubEventService _clubEventService;
        
        public ClubEventServiceTests(IClubEventService clubEventService)
        {
            _clubEventService = clubEventService;
        }
    }
}

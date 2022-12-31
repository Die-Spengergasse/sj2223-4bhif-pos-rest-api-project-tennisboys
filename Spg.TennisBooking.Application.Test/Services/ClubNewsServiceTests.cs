using Spg.TennisBooking.Domain.Interfaces;

namespace Spg.TennisBooking.Application.Services
{
    public class ClubNewsService : IClubNewsService
    {
        private readonly IClubNewsRepository _clubNewsRepository;

        public ClubNewsService(IClubNewsRepository clubNewsRepository)
        {
            _clubNewsRepository = clubNewsRepository;
        }
    }
}

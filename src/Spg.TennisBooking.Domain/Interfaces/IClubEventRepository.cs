using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IClubEventRepository
    {
        ClubEvent? Get(int id);
        IEnumerable<ClubEvent> GetAll(Club club);
        void Add(ClubEvent clubEvent);
        void Update(ClubEvent clubEvent);
        void Delete(ClubEvent clubEvent);
    }
}

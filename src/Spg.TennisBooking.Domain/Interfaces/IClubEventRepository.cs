using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IClubEventRepository
    {
        Task<ClubEvent?> Get(int id);
        Task<IEnumerable<ClubEvent>> GetAll(Club club);
        void Add(ClubEvent clubEvent);
        void Update(ClubEvent clubEvent);
        void Delete(ClubEvent clubEvent);
    }
}

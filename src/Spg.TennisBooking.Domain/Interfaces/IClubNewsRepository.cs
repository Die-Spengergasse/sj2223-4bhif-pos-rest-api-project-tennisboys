using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IClubNewsRepository
    {
        void Add(ClubNews news);
        void Delete(ClubNews news);
        void Update(ClubNews news);
        Task<ClubNews?> Get(int id);
        Task<IEnumerable<ClubNews>> GetAll(Club club);
    }
}

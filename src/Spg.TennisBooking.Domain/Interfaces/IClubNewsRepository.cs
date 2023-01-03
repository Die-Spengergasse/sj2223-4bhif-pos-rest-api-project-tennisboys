using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IClubNewsRepository
    {
        void Add(ClubNews news);
        void Delete(ClubNews news);
        void Update(ClubNews news);
        ClubNews? Get(int id);
        IEnumerable<ClubNews> GetAll();
    }
}

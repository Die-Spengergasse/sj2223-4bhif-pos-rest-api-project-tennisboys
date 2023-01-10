using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IClubRepository
    {
        Task<Club?> Get(int id);
        Task<Club?> GetByLink(string link);
        Task<IEnumerable<Club>> GetAll(string search);
        void Update(Club club);
        void Delete(Club club);
        void Add(Club club);
    }
}

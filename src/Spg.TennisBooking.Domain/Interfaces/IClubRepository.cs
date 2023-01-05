using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IClubRepository
    {
        Club? GetById(int id);
        Task<Club?> GetByLink(string link);
        void Update(Club club);
        void Delete(Club club);
        void Add(Club club);
    }
}

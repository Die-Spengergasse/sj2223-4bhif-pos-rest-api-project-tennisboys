using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface ICourtRepository
    {
        Court? GetByUUID(string uuid);
        void Add(Court court);
        void Update(Court court);
        void Delete(Court court);
        IEnumerable<Court> GetAll(Club club);
    }
}

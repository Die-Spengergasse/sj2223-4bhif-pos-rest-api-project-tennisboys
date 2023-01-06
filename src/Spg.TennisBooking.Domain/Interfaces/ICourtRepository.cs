using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface ICourtRepository
    {
        Task<Court?> Get(int id);
        void Add(Court court);
        void Update(Court court);
        void Delete(Court court);
        Task<IEnumerable<Court>> GetAll(Club club);
    }
}

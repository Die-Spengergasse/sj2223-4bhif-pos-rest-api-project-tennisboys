using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation?> GetByUUID(string uuid);
        void Add(Reservation reservation);
        void Update(Reservation reservation);
        void Delete(Reservation reservation);
        Task<IEnumerable<Reservation>> GetByCourtAndDateRange(Court court, DateTime from, DateTime to);
        Task<IEnumerable<Reservation>> GetByUser(User user);
        Task<IEnumerable<Reservation>> GetByClub(Club club);
    }
}

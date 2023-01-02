using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IReservationRepository
    {
        Reservation? GetByUUID(string uuid);
        Reservation Add(Reservation reservation);
        bool Update(Reservation reservation);
        bool Delete(Reservation reservation);
        IEnumerable<Reservation> GetByCourtAndDateRange(Court court, DateTime from, DateTime to);
        IEnumerable<Reservation> GetByUser(User user);
        IEnumerable<Reservation> GetByClub(Club club);
    }
}

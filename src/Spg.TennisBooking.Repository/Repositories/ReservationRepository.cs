using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;
using Spg.TennisBooking.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Repository.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly TennisBookingContext _db;

        public ReservationRepository(TennisBookingContext db)
        {
            _db = db;
        }
        
        public Reservation Add(Reservation reservation)
        {
            _db.Reservations.Add(reservation);
            _db.SaveChanges();
            return reservation;
        }

        public bool Delete(Reservation reservation)
        {
            _db.Reservations.Remove(reservation);
            _db.SaveChanges();
            return true;
        }

        public IEnumerable<Reservation> GetByClub(Club club)
        {
            return _db.Reservations.Where(r => r.CourtNavigation != null && r.CourtNavigation.ClubNavigation == club);
        }

        public IEnumerable<Reservation> GetByCourtAndDateRange(Court court, DateTime from, DateTime to)
        {
            return _db.Reservations.Where(r => r.CourtNavigation == court && r.StartTime <= to && r.EndTime >= from);
        }

        public IEnumerable<Reservation> GetByUser(User user)
        {
            return _db.Reservations.Where(r => r.UserNavigation == user);
        }

        public Reservation? GetByUUID(string uuid)
        {
            return _db.Reservations.FirstOrDefault(r => r.UUID == uuid);
        }

        public bool Update(Reservation reservation)
        {
            _db.Reservations.Update(reservation);
            _db.SaveChanges();
            return true;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;
using Spg.TennisBooking.Infrastructure.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Repository.Repositories.v1
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly TennisBookingContext _db;

        public ReservationRepository(TennisBookingContext db)
        {
            _db = db;
        }
        
        public void Add(Reservation reservation)
        {
            _db.Reservations.Add(reservation);
            _db.SaveChanges();
        }

        public void Delete(Reservation reservation)
        {
            _db.Reservations.Remove(reservation);
            _db.SaveChanges();
        }

        public async Task<IEnumerable<Reservation>> GetByClub(Club club)
        {
            return await _db.Reservations.Where(r => r.CourtNavigation != null && r.CourtNavigation.ClubNavigation == club).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetByCourtAndDateRange(Court court, DateTime from, DateTime to)
        {
            return await _db.Reservations.Where(r => r.CourtNavigation == court && r.StartTime <= to && r.EndTime >= from).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetByUser(User user)
        {
            return await _db.Reservations.Where(r => r.UserNavigation == user).ToListAsync();
        }

        public async Task<Reservation?> GetByUUID(string uuid)
        {
            return await _db.Reservations.FirstOrDefaultAsync(r => r.UUID == uuid);
        }

        public void Update(Reservation reservation)
        {
            _db.Reservations.Update(reservation);
            _db.SaveChanges();
        }
    }
}

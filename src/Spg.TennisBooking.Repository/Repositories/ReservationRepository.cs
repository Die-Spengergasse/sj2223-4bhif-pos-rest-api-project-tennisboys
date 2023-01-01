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
            throw new NotImplementedException();
        }

        public bool Delete(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reservation> GetByClub(Club club)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reservation> GetByCourtAndDateRange(Court court, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reservation> GetByUser(User user)
        {
            throw new NotImplementedException();
        }

        public Reservation GetByUUID(string uuid)
        {
            throw new NotImplementedException();
        }

        public bool Update(Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}

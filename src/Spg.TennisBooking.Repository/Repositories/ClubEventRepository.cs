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
    public class ClubEventRepository : IClubEventRepository
    {
        private readonly TennisBookingContext _db;

        public ClubEventRepository(TennisBookingContext db)
        {
            _db = db;
        }

        public void Add(ClubEvent clubEvent)
        {
            _db.ClubEvents.Add(clubEvent);
        }

        public void Delete(ClubEvent clubEvent)
        {
            _db.ClubEvents.Remove(clubEvent);
        }

        public ClubEvent? Get(int id)
        {
            return _db.ClubEvents.Find(id);
        }

        public IEnumerable<ClubEvent> GetAll(Club club)
        {
            return _db.ClubEvents.Where(x => x.ClubNavigation == club);
        }

        public void Update(ClubEvent clubEvent)
        {
            _db.ClubEvents.Update(clubEvent);
        }
    }
}

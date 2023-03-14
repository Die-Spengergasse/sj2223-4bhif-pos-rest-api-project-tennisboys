using Microsoft.EntityFrameworkCore;
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
            _db.SaveChanges();
        }

        public void Delete(ClubEvent clubEvent)
        {
            _db.ClubEvents.Remove(clubEvent);
            _db.SaveChanges();
        }

        public async Task<ClubEvent?> Get(int id)
        {
            return await _db.ClubEvents.FindAsync(id);
        }

        public async Task<IEnumerable<ClubEvent>> GetAll(Club club)
        {
            return await _db.ClubEvents.Where(x => x.ClubNavigation == club).ToListAsync();
        }

        public void Update(ClubEvent clubEvent)
        {
            _db.ClubEvents.Update(clubEvent);
            _db.SaveChanges();
        }
    }
}

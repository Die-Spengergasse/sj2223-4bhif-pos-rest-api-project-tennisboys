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
    public class ClubRepository : IClubRepository
    {
        private readonly TennisBookingContext _db;

        public ClubRepository(TennisBookingContext db)
        {
            _db = db;
        }

        public void Add(Club club)
        {
            _db.Clubs.Add(club);
            _db.SaveChanges();
        }

        public void Delete(Club club)
        {
            _db.Clubs.Remove(club);
            _db.SaveChanges();
        }

        public async Task<Club?> Get(int id)
        {
            return await _db.Clubs.FindAsync(id);
        }

        public async Task<Club?> GetByLink(string link)
        {
            return await _db.Clubs.FirstOrDefaultAsync(c => c.Link == link);
        }

        public async Task<IEnumerable<Club>> GetAll(string search)
        {
            return await _db.Clubs.Where(c => c.Name.Contains(search)).ToListAsync();
        }

        public void Update(Club club)
        {
            _db.Clubs.Update(club);
            _db.SaveChanges();
        }
    }
}

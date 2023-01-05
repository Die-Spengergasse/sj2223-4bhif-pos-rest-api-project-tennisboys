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

        public Club? GetById(int id)
        {
            return _db.Clubs.Find(id);
        }

        public async Task<Club?> GetByLink(string link)
        {
            return await _db.Clubs.FirstOrDefaultAsync(c => c.Link == link);
        }

        public void Update(Club club)
        {
            _db.Clubs.Update(club);
            _db.SaveChanges();
        }
    }
}

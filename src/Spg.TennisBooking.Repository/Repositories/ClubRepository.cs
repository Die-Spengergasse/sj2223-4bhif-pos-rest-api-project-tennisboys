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

        public bool Delete(Club club)
        {
            _db.Clubs.Remove(club);
            _db.SaveChanges();
            return true;
        }

        public Club? GetById(int id)
        {
            return _db.Clubs.Find(id);
        }

        public Club? GetByLink(string link)
        {
            return _db.Clubs.FirstOrDefault(c => c.Link == link);
        }

        public bool Update(Club club)
        {
            _db.Clubs.Update(club);
            _db.SaveChanges();
            return true;
        }
    }
}

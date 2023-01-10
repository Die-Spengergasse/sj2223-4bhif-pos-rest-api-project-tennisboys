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
    public class ClubNewsRepository : IClubNewsRepository
    {
        private readonly TennisBookingContext _db;

        public ClubNewsRepository(TennisBookingContext db)
        {
            _db = db;
        }

        public void Add(ClubNews news)
        {
            _db.ClubNews.Add(news);
        }

        public void Delete(ClubNews news)
        {
            _db.ClubNews.Remove(news);
        }

        public async Task<ClubNews?> Get(int id)
        {
            return await _db.ClubNews.FindAsync(id);
        }

        public async Task<IEnumerable<ClubNews>> GetAll(Club club)
        {
            return await _db.ClubNews.Where(x => x.ClubNavigation == club).ToListAsync();
        }

        public void Update(ClubNews news)
        {
            _db.ClubNews.Update(news);
        }
    }
}

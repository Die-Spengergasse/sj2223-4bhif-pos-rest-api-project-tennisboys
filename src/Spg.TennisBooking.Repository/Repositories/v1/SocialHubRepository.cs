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
    public class SocialHubRepository : ISocialHubRepository
    {
        private readonly TennisBookingContext _db;

        public SocialHubRepository(TennisBookingContext db)
        {
            _db = db;
        }

        public void Add(SocialHub socialHub)
        {
            _db.SocialHubs.Add(socialHub);
            _db.SaveChanges();
        }

        public void Delete(SocialHub socialHub)
        {
            _db.SocialHubs.Remove(socialHub);
            _db.SaveChanges();
        }

        public void Update(SocialHub socialHub)
        {
            _db.SocialHubs.Update(socialHub);
            _db.SaveChanges();
        }
    }
}

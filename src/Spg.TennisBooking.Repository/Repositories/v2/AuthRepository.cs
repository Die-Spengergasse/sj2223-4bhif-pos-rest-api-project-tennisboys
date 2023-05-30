using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;
using Spg.TennisBooking.Infrastructure.v2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Repository.Repositories.v2
{
    public class AuthRepository : IAuthRepository
    {
        private readonly TennisBookingContext _db;

        public AuthRepository(TennisBookingContext db)
        {
            _db = db;
        }
        
        public User Create(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public User? GetByEmail(string email)
        {
            return _db.Users.FirstOrDefault(u => u.Email == email);
        }

        public User? GetByUUIDold(string uuid)
        {
            return _db.Users.FirstOrDefault(u => u.UUID == uuid);
        }

        public bool Update(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();

            return true;
        }
    }
}

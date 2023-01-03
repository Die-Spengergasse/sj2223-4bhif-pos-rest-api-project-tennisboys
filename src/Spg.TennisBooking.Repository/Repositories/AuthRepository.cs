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
    public class AuthRepository : IAuthRepository
    {
        private readonly TennisBookingContext _db;

        public AuthRepository(TennisBookingContext db)
        {
            _db = db;
        }

        public User CreateUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public User? GetUserByEmail(string email)
        {
            return _db.Users.FirstOrDefault(u => u.Email == email);
        }

        public User? GetUserByUuid(string uuid)
        {
            return _db.Users.FirstOrDefault(u => u.UUID == uuid);
        }

        public bool UpdateUser(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();

            return true;
        }
    }
}

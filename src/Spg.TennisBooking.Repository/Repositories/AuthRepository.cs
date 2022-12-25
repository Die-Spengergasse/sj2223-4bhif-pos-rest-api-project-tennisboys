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

        public User CreateUser(string email, string hashedPassword)
        {
            User user = new(email, hashedPassword);
            _db.Users.Add(user);
            _db.SaveChanges();

            return user;
        }
    }
}

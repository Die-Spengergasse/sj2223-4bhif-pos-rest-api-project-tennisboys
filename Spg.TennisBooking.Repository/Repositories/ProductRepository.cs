using Microsoft.Extensions.DependencyInjection;
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
    public class ProductRepository : IProductRepository
    {
        private readonly TennisBookingContext _db;

        public ProductRepository(TennisBookingContext db)
        {
            _db = db;
        }
        public IEnumerable<User> GetAll()
        {
            return _db.Users;
        }

        public User GetByName(string name)
        {
            return _db.Users.SingleOrDefault(p => p.FirstName == name) 
                ?? throw new KeyNotFoundException("");
        }
    }
}

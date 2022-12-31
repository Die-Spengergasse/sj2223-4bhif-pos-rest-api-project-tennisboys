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
    public class CourtRepository : ICourtRepository
    {
        private readonly TennisBookingContext _db;

        public CourtRepository(TennisBookingContext db)
        {
            _db = db;
        }
    }
}
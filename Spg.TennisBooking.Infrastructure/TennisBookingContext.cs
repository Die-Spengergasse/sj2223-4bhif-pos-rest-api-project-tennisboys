using Microsoft.EntityFrameworkCore;

namespace Spg.TennisBooking.Infrastructure
{
    public class TennisBookingContext : DbContext
    {
        protected TennisBookingContext() : this(new DbContextOptions<DbContext>())
        {
        }

        public TennisBookingContext(DbContextOptions options) : base(options)
        {
        }
    }
}
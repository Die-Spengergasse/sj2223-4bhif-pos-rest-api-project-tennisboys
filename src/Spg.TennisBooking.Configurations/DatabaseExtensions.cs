using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Spg.TennisBooking.Infrastructure;

namespace Spg.TennisBooking.Extensions
{
    public static class DatabaseExtensions
    {
        public static void ConfigureSqLite(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TennisBookingContext>(options => 
            {
                if (!options.IsConfigured)
                {
                    options.UseSqlite(connectionString);
                }
            });
        }
    }
}
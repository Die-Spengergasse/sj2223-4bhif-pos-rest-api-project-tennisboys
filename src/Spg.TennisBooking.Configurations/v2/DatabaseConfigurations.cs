using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Spg.TennisBooking.Infrastructure.v2;

namespace Spg.TennisBooking.Configurations.v2
{
    public static class DatabaseConfigurations
    {
        public static void ConfigureDB(this IServiceCollection services, string connectionString, string useDb)
        {
            services.AddDbContext<TennisBookingContext>(options => 
            {
                if (!options.IsConfigured)
                {
                    if (useDb == "MySQL")
                    {
                        options.UseMySQL(connectionString);
                    }
                    else if (useDb == "SQLite")
                    {
                        options.UseSqlite(connectionString);
                    }
                    else
                    {
                        throw new Exception("No Database selected");
                    }
                    //options.UseLazyLoadingProxies();
                }
            });
        }
    }
}
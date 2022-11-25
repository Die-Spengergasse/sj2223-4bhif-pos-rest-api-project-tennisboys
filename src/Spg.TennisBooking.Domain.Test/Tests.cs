using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spg.TennisBooking.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Test
{
    public class Tests
    {
        protected TennisBookingContext GetContext()
        {
            //Generate random database name
            string dbName = Guid.NewGuid().ToString();

            string data_source = "Data Source=" + dbName + ".db";

            DbContextOptions options = new DbContextOptionsBuilder()
            .UseSqlite(data_source)
            .Options;

            TennisBookingContext db = new TennisBookingContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }
        protected Club CreateClub()
        {
            User user = CreateUser();
            Club club = new Club(user, DateTime.Now, "Tennisclub", "Tennisclub in der Nähe", "Musterstrasse 1", "1234", "imagepath");
            return club;
        }
        
        protected User CreateUser()
        {
            User user = new User("Max", "Mustermann", GenderTypes.Male, "Musterstrasse 1", "adrian.schauer@aon.at", new PhoneNumber("0664", "1234567"), new DateTime(1990, 1, 1));
            return user;
        }
    }
}

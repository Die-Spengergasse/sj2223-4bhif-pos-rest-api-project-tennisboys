using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spg.TennisBooking.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Test.v1
{
    public class Tests
    {
        protected static TennisBookingContext GetContext()
        {
            //Generate random database name
            string dbName = Guid.NewGuid().ToString();

            string data_source = "Data Source=" + dbName + ".db";

            DbContextOptions options = new DbContextOptionsBuilder()
            .UseSqlite(data_source)
            .Options;

            TennisBookingContext db = new(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }
        protected static Club CreateClub()
        {
            User user = CreateUser();
            Club club = new("TC Eichgraben", user);
            return club;
        }
        
        protected static User CreateUser()
        {
            User user = new("adrian.schauer@aon.at", "AdminPswd", "012345");
            return user;
        }
    }
}

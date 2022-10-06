using Microsoft.EntityFrameworkCore;
using Spg.TennisBooking.Domain.Model;
using Spg.TennisBooking.Infrastructure;
using System;
using System.Linq;
using Xunit;

namespace Spg.TennisBooking.Domain.Test
{
    public class DatabaseTests
    {
        private TennisBookingContext GetContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
            .UseSqlite("Data Source=TennisBookingTest.db")
            .Options;

            TennisBookingContext db = new TennisBookingContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void DomainModel_Create_Club_Success_Test()
        {
            TennisBookingContext db = GetContext();

            Club newClub = new Club()
            {
                Name = "Tennis Club 1",
                Info = "Tennis Club 1 Info",
                ImagePath = "TennisClub1.jpg"
            };

            db.Clubs.Add(newClub);
            db.SaveChanges();

            Assert.Equal(1, db.Clubs.Count());
        }

        [Fact]
        public void DomainModel_Create_Customer_Success_Test()
        {
            TennisBookingContext db = GetContext();
            
            Customer newCustomer = new Customer()
            {
                FirstName="TestFirstName",
                LastName="TestLastName",
                Gender=Gender.Male,
                Address = "TestAddress",
                Email = "xy@gmail.at",
                PhoneNumber = "123456789",
            };

            db.Customers.Add(newCustomer);
            db.SaveChanges();

            Assert.Equal(1, db.Customers.Count());
        }

        [Fact]
        public void DomainModel_AddCourtToClub_Success_Test()
        {
            TennisBookingContext db = GetContext();

            Court newCourt = new Court()
            {
                Occupied = true,
                Type = "Grass",
                Price = 10.00
            };

            Club newClub = new Club()
            {
                Name = "Tennis Club 1",
                Info = "Tennis Club 1 Info",
                ImagePath = "TennisClub1.jpg"
            };

            newClub.AddCourt(newCourt);

            db.Clubs.Add(newClub);
            db.SaveChanges();

            Assert.Equal(1, db.Clubs.Count());
            Assert.Equal(1, db.Courts.Count());
            Assert.Single(db.Clubs.First().Courts);
        }

        [Fact]
        public void DomainModel_ChangeSocialHubPrpertyOnClub_Success_Test()
        {
            TennisBookingContext db = GetContext();

            Club newClub = new Club()
            {
                Name = "Tennis Club 1",
                Info = "Tennis Club 1 Info",
                ImagePath = "TennisClub1.jpg"
            };

            newClub.SocialHub.Facebook = "adrian";

            db.Clubs.Add(newClub);
            db.SaveChanges();

            Assert.Equal(1, db.Clubs.Count());
            Assert.Equal("adrian", newClub.SocialHub.Facebook);
        }
    }
}
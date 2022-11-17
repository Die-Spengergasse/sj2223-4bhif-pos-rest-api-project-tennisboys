using Microsoft.EntityFrameworkCore;
using Spg.TennisBooking.Domain.Model;
using Spg.TennisBooking.Infrastructure;
using System;
using System.Linq;
using Xunit;

namespace Spg.TennisBooking.Domain.Test
{
    public class EntityTests : Tests
    {
        //TESTS
        //All creation of Entities should be tested here. One should work another should fail.

        //Club
        [Fact]
        public void DomainModel_Create_Club_Success_Test()
        {
            TennisBookingContext db = GetContext();

            Club newClub = CreateClub();

            db.Clubs.Add(newClub);
            db.SaveChanges();

            Assert.Equal(1, db.Clubs.Count());
        }

        //ClubEvent
        [Fact]
        public void DomainModel_Create_ClubEvent_Success_Test()
        {
            TennisBookingContext db = GetContext();

            Club newClub = CreateClub();

            ClubEvent newClubEvent = new ClubEvent(newClub, "Club Event 1", DateTime.Now, "Club Event 1 Info");

            db.ClubEvents.Add(newClubEvent);
            db.SaveChanges();

            Assert.Equal(1, db.ClubEvents.Count());
        }

        //ClubNews
        [Fact]
        public void DomainModel_Create_ClubNews_Success_Test()
        {
            TennisBookingContext db = GetContext();

            ClubNews newClubNews = new ClubNews("Club News 1", "Club News 1 Info", CreateClub());

            db.ClubNews.Add(newClubNews);
            db.SaveChanges();

            Assert.Equal(1, db.ClubNews.Count());
        }

        //Court
        [Fact]
        public void DomainModel_Create_Court_Success_Test()
        {
            TennisBookingContext db = GetContext();

            Court newCourt = new Court(CourtType.Hard, "Court 1", CreateClub(),
            20, 20, 5, 15, 12);

            db.Courts.Add(newCourt);
            db.SaveChanges();

            Assert.Equal(1, db.Courts.Count());
        }

        //Customer
        [Fact]
        public void DomainModel_Create_Customer_Success_Test()
        {
            TennisBookingContext db = GetContext();

            Customer newCustomer = CreateCustomer();

            db.Customers.Add(newCustomer);
            db.SaveChanges();

            Assert.Equal(1, db.Customers.Count());
        }

        //PhoneNumber
        // [Fact]
        // public void DomainModel_Create_PhoneNumber_Success_Test()
        // {
        //     TennisBookingContext db = GetContext();

        //     PhoneNumber newPhoneNumber = new PhoneNumber("0664", "1234567");

        //     db.PhoneNumbers.Add(newPhoneNumber);
        //     db.SaveChanges();

        //     Assert.Equal(1, db.PhoneNumbers.Count());
        // }

        //Reservation
        [Fact]
        public void DomainModel_Create_Reservation_Success_Test()
        {
            TennisBookingContext db = GetContext();

            Reservation newReservation = new Reservation(CreateClub(),
            DateTime.Now, 22, 23,
            new Court(CourtType.Hard, "Court 1", CreateClub(),
            20, 20, 5, 15, 12),
            CreateCustomer());

            db.Reservations.Add(newReservation);
            db.SaveChanges();

            Assert.Equal(1, db.Reservations.Count());
        }
        //SocialHub
        [Fact]
        public void DomainModel_Create_SocialHub_Success_Test()
        {
            TennisBookingContext db = GetContext();

            SocialHub newSocialHub = new SocialHub();

            db.SocialHubs.Add(newSocialHub);
            db.SaveChanges();

            Assert.Equal(1, db.SocialHubs.Count());
        }

        //Trainer        
        [Fact]
        public void DomainModel_Create_Trainer_Success_Test()
        {
            TennisBookingContext db = GetContext();

            Trainer newTrainer = new Trainer(
                CreateClub(), "Trainer Firstname", "Trainer Lastname", GenderTypes.Male, "Trainer Musterstrasse 1", 1321, "SomeImage");

            db.Trainers.Add(newTrainer);
            db.SaveChanges();

            Assert.Equal(1, db.Trainers.Count());
        }

        /*

        [Fact]
        public void DomainModel_AddCourtToClub_Success_Test()
        {
            TennisBookingContext db = GetContext();

            Court newCourt = new Court()
            {
                
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
        */
    }
}
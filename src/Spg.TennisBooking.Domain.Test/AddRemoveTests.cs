using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Spg.TennisBooking.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Test
{
    public class AddRemoveTests : Tests
    {
        //AddClubNews
        [Fact]
        public void DomainModel_Club_AddClubNews_Test()
        {
            // Arrange
            TennisBookingContext db = GetContext();
            Club club = CreateClub();
            ClubNews clubNews = new ClubNews("Club News 1", "Club News 1 Info", club);

            // Act
            club.AddClubNews(clubNews);

            db.Clubs.Add(club);
            db.SaveChanges();

            // Assert
            Assert.Single(club.ClubNews);

            db.Database.EnsureDeleted();
        }

        //RemoveClubNews
        [Fact]
        public void DomainModel_Club_RemoveClubNews_Test()
        {
            // Arrange
            TennisBookingContext db = GetContext();
            Club club = CreateClub();
            ClubNews clubNews = new ClubNews("Club News 1", "Club News 1 Info", club);
            club.AddClubNews(clubNews);

            db.Clubs.Add(club);
            db.SaveChanges();

            //Assert
            Assert.Single(club.ClubNews);

            // Act
            club.RemoveClubNews(clubNews);

            db.SaveChanges();

            // Assert
            Assert.Empty(club.ClubNews);

            db.Database.EnsureDeleted();
        }

        //AddClubEvent
        [Fact]
        public void DomainModel_Club_AddClubEvent_Test()
        {
            // Arrange
            TennisBookingContext db = GetContext();
            Club club = CreateClub();
            ClubEvent clubEvent = new ClubEvent(club, "Club Event 1", DateTime.Now, "Club Event 1 Info");

            // Act
            club.AddClubEvent(clubEvent);

            db.Clubs.Add(club);
            db.SaveChanges();

            // Assert
            Assert.Single(club.ClubEvents);

            db.Database.EnsureDeleted();
        }

        //RemoveClubEvent
        [Fact]
        public void DomainModel_Club_RemoveClubEvent_Test()
        {
            // Arrange
            TennisBookingContext db = GetContext();
            Club club = CreateClub();
            ClubEvent clubEvent = new ClubEvent(club, "Club Event 1", DateTime.Now, "Club Event 1 Info");
            club.AddClubEvent(clubEvent);

            db.Clubs.Add(club);
            db.SaveChanges();

            //Assert
            Assert.Single(club.ClubEvents);

            // Act
            club.RemoveClubEvent(clubEvent);

            db.SaveChanges();

            // Assert
            Assert.Empty(club.ClubEvents);

            db.Database.EnsureDeleted();
        }

        //AddCourt
        [Fact]
        public void DomainModel_Club_AddCourt_Test()
        {
            // Arrange
            TennisBookingContext db = GetContext();
            Club club = CreateClub();
            Court court = new Court(CourtType.Carpet, "Court 1", club, 10, 10, 10, 10, 10);

            // Act
            club.AddCourt(court);

            db.Clubs.Add(club);
            db.SaveChanges();

            // Assert
            Assert.Single(club.Courts);

            db.Database.EnsureDeleted();
        }

        //RemoveCourt
        [Fact]
        public void DomainModel_Club_RemoveCourt_Test()
        {
            // Arrange
            TennisBookingContext db = GetContext();
            Club club = CreateClub();
            Court court = new Court(CourtType.Carpet, "Court 1", club, 10, 10, 10, 10, 10);
            club.AddCourt(court);
            db.Clubs.Add(club);
            db.SaveChanges();

            //Assert
            Assert.Single(club.Courts);

            // Act
            club.RemoveCourt(court);
            db.SaveChanges();

            // Assert
            Assert.Empty(club.Courts);

            db.Database.EnsureDeleted();
        }

        //AddTrainer
        [Fact]
        public void DomainModel_Club_AddTrainer_Test()
        {
            // Arrange
            TennisBookingContext db = GetContext();
            Club club = CreateClub();
            Trainer trainer = new Trainer(club, "Max", "Mustermann", GenderTypes.Male, "Bester Mann", 16, "img/Trainer/MaxMustermann.jpg");

            // Act
            club.AddTrainer(trainer);

            db.Clubs.Add(club);
            db.SaveChanges();

            // Assert
            Assert.Single(club.Trainers);

            db.Database.EnsureDeleted();
        }

        //RemoveTrainer
        [Fact]
        public void DomainModel_Club_RemoveTrainer_Test()
        {
            // Arrange
            TennisBookingContext db = GetContext();
            Club club = CreateClub();
            Trainer trainer = new Trainer(club, "Max", "Mustermann", GenderTypes.Male, "Bester Mann", 16, "img/Trainer/MaxMustermann.jpg");

            club.AddTrainer(trainer);

            db.Clubs.Add(club);
            db.SaveChanges();

            //Assert
            Assert.Single(club.Trainers);

            // Act
            club.RemoveTrainer(trainer);
            db.SaveChanges();

            // Assert
            Assert.Empty(club.Trainers);

            db.Database.EnsureDeleted();
        }

        //AddReservation
        [Fact]
        public void DomainModel_Court_AddReservation_Test()
        {
            // Arrange
            TennisBookingContext db = GetContext();
            Club club = CreateClub();
            Court court = new Court(CourtType.Carpet, "Court 1", club, 10, 10, 10, 10, 10);
            User user = CreateUser();

            club.AddCourt(court);


            Reservation res = new Reservation(club, DateTime.Now, 10, 12, court, user);

            // Act
            court.AddReservation(res);

            db.Clubs.Add(club);
            db.SaveChanges();

            // Assert
            Assert.Single(court.Reservations);

            db.Database.EnsureDeleted();
        }

        //RemoveReservation
        [Fact]
        public void DomainModel_Court_RemoveReservation_Test()
        {
            // Arrange
            TennisBookingContext db = GetContext();
            Club club = CreateClub();
            Court court = new Court(CourtType.Carpet, "Court 1", club, 10, 10, 10, 10, 10);
            User user = CreateUser();

            club.AddCourt(court);
            
            db.Clubs.Add(club);

            Reservation res = new Reservation(club, DateTime.Now, 10, 12, court, user);

            court.AddReservation(res);
            db.SaveChanges();
            
            //Assert
            Assert.Single(court.Reservations);

            // Act
            court.RemoveReservation(res);
            db.SaveChanges();

            // Assert
            Assert.Empty(court.Reservations);

            db.Database.EnsureDeleted();
        }
    }
}

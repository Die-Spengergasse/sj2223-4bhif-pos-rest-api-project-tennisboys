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

            //Assert
            Assert.Single(club.ClubNews);

            // Act
            club.RemoveClubNews(clubNews);

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

            //Assert
            Assert.Single(club.ClubEvents);

            // Act
            club.RemoveClubEvent(clubEvent);

            // Assert
            Assert.Empty(club.ClubEvents);
            
            db.Database.EnsureDeleted();
        }

        //
    }
}

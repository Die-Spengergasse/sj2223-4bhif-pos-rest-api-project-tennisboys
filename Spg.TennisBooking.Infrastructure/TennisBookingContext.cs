using Microsoft.EntityFrameworkCore;
using Spg.TennisBooking.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Infrastructure
{
    public class TennisBookingContext : DbContext
    {
        public DbSet<Club> Clubs => Set<Club>();
        public DbSet<ClubEvent> ClubEvents => Set<ClubEvent>();
        public DbSet<ClubNews> ClubNews => Set<ClubNews>();
        public DbSet<Court> Courts => Set<Court>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<PhoneNumber> PhoneNumbers => Set<PhoneNumber>();
        public DbSet<SocialHub> SocialHubs => Set<SocialHub>();
        public DbSet<Trainer> Trainers => Set<Trainer>();


        protected TennisBookingContext() : this(new DbContextOptions<DbContext>())
        {
        }

        public TennisBookingContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=TennisBooking.db");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().HasKey(e => e.Name);
            modelBuilder.Entity<User>().OwnsOne(p => p.PhoneNumber);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Spg.TennisBooking.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using System.Security.Cryptography;

namespace Spg.TennisBooking.Infrastructure.v2
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
        public void Seed()
        {
            // db.Database.EnsureCreated();

            //User
            List<User> users = GetSeedingUsers();
            Users.AddRange(users);
            SaveChanges();

            //Club
            List<Club> clubs = GetSeedingClubs(users);
            Clubs.AddRange(clubs);
            SaveChanges();

            //ClubEvent
            ClubEvents.AddRange(GetSeedingClubEvents(clubs));
            SaveChanges();

            //ClubNews
            ClubNews.AddRange(GetSeedingClubNews(clubs));
            SaveChanges();

            //Court
            List<Court> courts = GetSeedingCourts(clubs);
            Courts.AddRange(courts);
            SaveChanges();

            //Reservation
            Reservations.AddRange(GetSeedingReservations(courts, users, clubs));
            SaveChanges();

            //Trainer
            Trainers.AddRange(GetSeedingTrainers(clubs));
            SaveChanges();
        }

        public static string HashPassword(string password)
        {
            //https://stackoverflow.com/questions/4181198/how-to-hash-a-password
            byte[] salt;
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }

        private List<User> GetSeedingUsers()
        {
            List<User> users = new Faker<User>("de").CustomInstantiator(f =>
                new User(
                    f.Internet.Email(),
                    HashPassword("123456"),
                    "654321"))
                .Generate(30)
                .ToList();
            return users;
        }

        private List<Club> GetSeedingClubs(List<User> users)
        {
            List<Club> clubs = new Faker<Club>("de").CustomInstantiator(f =>
                new Club(
                    f.Company.CompanyName(),
                    f.Random.ListItem(users)//.FirstOrDefault(s => s.Id == f.Random.Int(0, 10))
                ))
                .Generate(30)
                .ToList();
            return clubs;
        }

        private List<ClubEvent> GetSeedingClubEvents(List<Club> clubs)
        {
            List<ClubEvent> clubevents = new Faker<ClubEvent>("de").CustomInstantiator(f =>
                new ClubEvent(
                    f.Random.ListItem(clubs),//Clubs.FirstOrDefault(s => s.Id == f.Random.Int(0, 30)),
                    f.Company.Bs(),
                    f.Date.Recent(),
                    f.Lorem.Word()
                ))
                .Generate(100)
                .ToList();
            return clubevents;
        }

        private List<ClubNews> GetSeedingClubNews(List<Club> clubs)
        {
             List<ClubNews> clubnews = new Faker<ClubNews>("de").CustomInstantiator(f =>
                new ClubNews(
                    f.Company.CatchPhrase(),
                    f.Lorem.Word(),
                    f.Random.ListItem(clubs)//Clubs.Single(s => s.Id == f.Random.Int(0,30))
                ))
                .Generate(100)
                .ToList();
            return clubnews;
        }

        private List<Court> GetSeedingCourts(List<Club> clubs)
        {
            List<Court> courts = new Faker<Court>("de").CustomInstantiator(f =>
                new Court(
                    f.Random.ListItem(clubs),//Clubs.FirstOrDefault(s => s.Id == f.Random.Int(0,30)),
                    (CourtType)f.Random.Int(0, 3),
                    f.Random.Int(1, 30).ToString(),
                    f.Random.Double(10.0, 15.0),
                    f.Random.Double(15.0, 20.0),
                    f.Random.Int(0, 23),
                    f.Random.Int(0, 23),
                    f.Random.Int(0, 23)
                ))
                .Generate(100)
                .ToList();
            return courts;
        }

        private List<Reservation> GetSeedingReservations(List<Court> courts, List<User> users, List<Club> clubs)
        {
            List<Reservation> reservations = new Faker<Reservation>("de").CustomInstantiator(f =>
                new Reservation(
                    f.Date.Recent(),
                    f.Date.Soon(),
                    f.Lorem.Word(),
                    f.Random.ListItem(courts),//Courts.FirstOrDefault(s => s.Id == f.Random.Int(0, 30)),
                    f.Random.ListItem(users),//Users.FirstOrDefault(s => s.Id == f.Random.Int(0, 30)),
                    f.Random.ListItem(clubs)//Clubs.FirstOrDefault(s => s.Id == f.Random.Int(0, 30))
                ))
                .Generate(100)
                .ToList();
            return reservations;
        }

        private List<Trainer> GetSeedingTrainers(List<Club> clubs)
        {
            List<Trainer> trainers = new Faker<Trainer>("de").CustomInstantiator(f =>
                new Trainer(
                    f.Random.ListItem(clubs),//Clubs.FirstOrDefault(s => s.Id == f.Random.Int(0, 30)),
                    f.Name.FirstName(),
                    f.Name.LastName(),
                    (GenderTypes)f.Random.Int(1, 3),
                    f.Lorem.Word(),
                    f.Random.Int(0, 23),
                    f.System.FilePath()
                ))
                .Generate(100)
                .ToList();
            return trainers;
        }
    }
}
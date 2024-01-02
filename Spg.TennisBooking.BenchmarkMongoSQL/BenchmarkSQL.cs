using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using MongoDB.Driver.Linq;
using Spg.TennisBooking.BenchmarkMongoSQL.Dtos;
using Spg.TennisBooking.Domain.Model;
using Spg.TennisBooking.Infrastructure.v2;
using System.Diagnostics;
using System.Text.Json;

namespace Spg.TennisBooking.BenchmarkMongoSQL
{
    public class BenchmarkSQL : BenchmarkStatic
    {
        public IActionResult Benching()
        {
            //Start Benchmark
            Console.WriteLine("Start Benchmark");
            Stopwatch stopwatch = new();
            stopwatch.Start();

            //Get Context
            TennisBookingContext db = GetContext();
            //Time for creating the context
            Console.WriteLine("Time for creating the context: "+stopwatch.ElapsedMilliseconds);

            //Mocking
            Console.WriteLine("Mocking");
            db = Mocking(db);
            //Time for mocking
            Console.WriteLine("Time for mocking: "+stopwatch.ElapsedMilliseconds);
            
            //CourtRequest
            Console.WriteLine("CourtRequest");
            CourtRequestDto courtRequestDto = CourtRequest(db);
            //Print out result as JSON
            Console.WriteLine("CourtRequest: "+JsonSerializer.Serialize(courtRequestDto));
            //Time for CourtRequest
            Console.WriteLine("Time for CourtRequest: "+stopwatch.ElapsedMilliseconds);

            //End Benchmark
            stopwatch.Stop();
            Console.WriteLine("End Benchmark: "+stopwatch.ElapsedMilliseconds);

            //Delete Database
            db.Database.EnsureDeleted();

            return new OkObjectResult(JsonSerializer.Serialize(courtRequestDto));
        }

        public CourtRequestDto CourtRequest(TennisBookingContext db)
        {
            //Make a request to get all courts of a club
            Club club = db.Clubs.Include(c => c.Courts).FirstOrDefault();
            CourtRequestDto courtRequestDto = new();

            //Map the courts to the CourtRequestDto
            courtRequestDto.ClubName = club.Name;
            courtRequestDto.KW = "KW 1";
            var dayFrom = new DateTime(2023, 12, 11);
            var dayTo = new DateTime(2023, 12, 17);
            courtRequestDto.Courts = new();
            foreach (Court court in club.Courts)
            {
                CourtDto courtDto = new();
                courtDto.Name = court.Name;
                courtDto.Days = new();
                List<string> days = new() { "Mo", "Di", "Mi", "Do", "Fr", "Sa", "So" };
                for (int i = 0; i < 7; i++)
                {
                    CourtDayDto dayDto = new();
                    dayDto.Name = days[i];
                    dayDto.Reservations = new();

                    List<Reservation> reservations = db.Reservations.Where(r => r.CourtNavigation == court && ((int)r.StartTime.DayOfWeek) == i && r.StartTime >= dayFrom && r.StartTime <= dayTo).ToList();
                    for (int j = 0; j < reservations.Count; j++)
                    {
                        ReservationDto reservationDto = new();
                        //reservationDto.From = "10:00";
                        reservationDto.From = reservations[j].StartTime.ToString("HH:mm");
                        //reservationDto.To = "11:00";
                        reservationDto.To = reservations[j].EndTime.ToString("HH:mm");

                        dayDto.Reservations.Add(reservationDto);
                    }
                    courtDto.Days.Add(dayDto);
                }
                courtRequestDto.Courts.Add(courtDto);
            }   

            return courtRequestDto;
        }

        public TennisBookingContext GetContext()
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

        public TennisBookingContext Mocking(TennisBookingContext db)
        {
            Club club = CreateClub();
            db.Clubs.Add(club);

            //Create 10 courts
            for (int i = 0; i < 10; i++)
            {
                Court court = new("Court " + (i+1), club);
                db.Courts.Add(court);
            }

            //Create 100 users
            for (int i = 0; i < 100; i++)
            {
                User user = CreateUser();
                db.Users.Add(user);
            }

            Random rand = new();
            //Create 1000 reservations
            for (int i = 0; i < 10000; i++)
            {
                //All dates of 2023, from and to have to be same day with one hour apart full hour 6-22 foreach reservation random days
                //randomize it
                int ranDate = rand.Next(0, 364);
                int ranHour = rand.Next(6, 20);
                //Console.WriteLine(ranDate + " " + ranHour);
                DateTime from = new DateTime(2023, 1, 1).AddDays(ranDate).AddHours(ranHour);
                DateTime to = from.AddHours(1);
                //Console.WriteLine(from.ToString("dd.MM.yyyy HH:mm") + " - " + to.ToString("dd.MM.yyyy HH:mm"));
                Reservation reservation = new(from, to, "", club.Courts.FirstOrDefault(), db.Users.FirstOrDefault() ,club);
                db.Reservations.Add(reservation);
            }

            db.SaveChanges();

            return db;
        }
    }
}
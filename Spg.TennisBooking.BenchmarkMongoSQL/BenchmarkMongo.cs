using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Bson;
//using MongoDB.Driver;
using Spg.TennisBooking.BenchmarkMongoSQL.Dtos;
using Spg.TennisBooking.Domain.ModelMongo;
using Spg.TennisBooking.Infrastructure.v2;
using System.Diagnostics;
using System.Text.Json;

namespace Spg.TennisBooking.BenchmarkMongoSQL
{
    //https://www.mongodb.com/docs/entity-framework/current/quick-start/
    public class BenchmarkMongo : BenchmarkStaticMongo
    {
        public IActionResult Benching()
        {
            //Start Benchmark
            Console.WriteLine("Start Benchmark");
            Stopwatch stopwatch = new();
            stopwatch.Start();

            //Get MongoDB client and database
            var mongoDB = GetContext();

            //Time for creating the client and getting the database
            Console.WriteLine("Time for creating the client and getting the database: " + stopwatch.ElapsedMilliseconds);

            //Mocking
            Console.WriteLine("Mocking");
            mongoDB = Mocking(mongoDB);
            //Time for mocking
            Console.WriteLine("Time for mocking: " + stopwatch.ElapsedMilliseconds);

            //CourtRequest
            Console.WriteLine("CourtRequest");
            CourtRequestDto courtRequestDto = CourtRequest(mongoDB);
            //Print out result as JSON
            Console.WriteLine("CourtRequest: " + JsonSerializer.Serialize(courtRequestDto));
            //Time for CourtRequest
            Console.WriteLine("Time for CourtRequest: " + stopwatch.ElapsedMilliseconds);

            //End Benchmark
            stopwatch.Stop();
            Console.WriteLine("End Benchmark: " + stopwatch.ElapsedMilliseconds);

            //Delete Database
            // MongoDB doesn't have a direct equivalent to 'EnsureDeleted'. You'll need to drop each collection manually or drop the whole database.
            mongoDB.DropCollection("Clubs");
            mongoDB.DropCollection("Courts");
            mongoDB.DropCollection("Users");
            mongoDB.DropCollection("Reservations");

            return new OkObjectResult(JsonSerializer.Serialize(courtRequestDto));
        }

        public CourtRequestDto CourtRequest(IMongoDatabase db)
        {
            var clubsCollection = db.GetCollection<Club>("Clubs");
            var reservationsCollection = db.GetCollection<Reservation>("Reservations");

            //Make a request to get all courts of a club
            Club club = clubsCollection.Find(_ => true).FirstOrDefault();
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

                    List<Reservation> reservations = reservationsCollection.Find(r => r.CourtNavigation == court && ((int)r.StartTime.DayOfWeek) == i && r.StartTime >= dayFrom && r.StartTime <= dayTo).ToList();
                    for (int j = 0; j < reservations.Count; j++)
                    {
                        ReservationDto reservationDto = new();
                        reservationDto.From = reservations[j].StartTime.ToString("HH:mm");
                        reservationDto.To = reservations[j].EndTime.ToString("HH:mm");

                        dayDto.Reservations.Add(reservationDto);
                    }
                    courtDto.Days.Add(dayDto);
                }
                courtRequestDto.Courts.Add(courtDto);
            }

            return courtRequestDto;
        }

        public IMongoDatabase GetContext()
        {

            var mongoClient = new MongoClient("mongodb://root:1234@localhost:27017");

            var mongoDatabase = mongoClient.GetDatabase("TennisBooking");

            //_clubCollection = mongoDatabase.GetCollection<Club>();


            //var dbClient = new MongoClient("mongodb://root:1234@localhost:27017");
            //IMongoDatabase db = dbClient.GetDatabase("TennisBooking");
            //var np = db.GetCollection<BsonDocument>("np");
            ////Generate random database name
            //export MONGODB_URI = 'mongodb+srv://root:1234@cluster0.abc.mongodb.net/?retryWrites=true&w=majority'

            //var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
            //if (connectionString == null)
            //{
            //    Console.WriteLine("You must set your 'MONGODB_URI' environment variable. To learn how to set it, see https://www.mongodb.com/docs/drivers/csharp/current/quick-start/#set-your-connection-string");
            //    Environment.Exit(0);
            //}
            //var client = new MongoClient(connectionString);
            //IMongoDatabase database = client.GetDatabase("TennisBooking");
            //DbContextOptions options = new DbContextOptionsBuilder()
            //.UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            //.Options;

            //TennisBookingContext db = new(options);

            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();
            //return db;
            return mongoDatabase;
        }

        public IMongoDatabase Mocking(IMongoDatabase db)
        {
            var database = db;
            var clubsCollection = database.GetCollection<Club>("Clubs");
            var courtsCollection = database.GetCollection<Court>("Courts");
            var usersCollection = database.GetCollection<User>("Users");
            var reservationsCollection = database.GetCollection<Reservation>("Reservations");

            clubsCollection.DeleteMany(_ => true);
            courtsCollection.DeleteMany(_ => true);
            usersCollection.DeleteMany(_ => true);
            reservationsCollection.DeleteMany(_ => true);

            Club club = CreateClub();
            clubsCollection.InsertOne(club);

            //Create 10 courts
            for (int i = 0; i < 10; i++)
            {
                Court court = new("Court " + i, club);
                courtsCollection.InsertOne(court);
            }

            //Create 100 users
            for (int i = 0; i < 100; i++)
            {
                User user = CreateUser();
                usersCollection.InsertOne(user);
            }

            //Create 1000 reservations
            for (int i = 0; i < 1000; i++)
            {
                Reservation reservation = new(DateTime.Now, DateTime.Now.AddHours(1), "", club.Courts.FirstOrDefault(), usersCollection.Find(_ => true).FirstOrDefault(), club);
                reservationsCollection.InsertOne(reservation);
            }

            return database;
        }
    }
}
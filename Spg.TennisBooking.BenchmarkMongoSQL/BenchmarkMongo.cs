using Microsoft.EntityFrameworkCore;
//using MongoDB.Bson;
//using MongoDB.Driver;
using Spg.TennisBooking.BenchmarkMongoSQL.Dtos;
using Spg.TennisBooking.Domain.Model;
using Spg.TennisBooking.Infrastructure.v2;

namespace Spg.TennisBooking.BenchmarkMongoSQL
{
    //https://www.mongodb.com/docs/entity-framework/current/quick-start/
    public class BenchmarkMongo : BenchmarkStatic, Benchmark
    {
        public void Benching()
        {
            throw new NotImplementedException();
        }

        public CourtRequestDto CourtRequest(TennisBookingContext db)
        {
            throw new NotImplementedException();
        }

        public TennisBookingContext GetContext()
        {
            //var dbClient = new MongoClient("mongodb://root:1234@localhost:27017");
            //IMongoDatabase db = dbClient.GetDatabase("nachpruefungen");
            //var np = db.GetCollection<BsonDocument>("np");
            //Generate random database name


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
            return null;
        }

        public TennisBookingContext Mocking(TennisBookingContext db)
        {
            Club club = CreateClub();
            db.Clubs.Add(club);

            //Create 10 courts
            for (int i = 0; i < 10; i++)
            {
                Court court = new("Court " + i, club);
                db.Courts.Add(court);
            }

            //Create 100 users
            for (int i = 0; i < 100; i++)
            {
                User user = CreateUser();
                db.Users.Add(user);
            }

            //Create 1000 reservations
            for (int i = 0; i < 1000; i++)
            {
                Reservation reservation = new(DateTime.Now, DateTime.Now.AddHours(1), "", club.Courts.FirstOrDefault(), db.Users.FirstOrDefault(), club);
                db.Reservations.Add(reservation);
            }

            db.SaveChanges();

            return db;
        }
    }
}
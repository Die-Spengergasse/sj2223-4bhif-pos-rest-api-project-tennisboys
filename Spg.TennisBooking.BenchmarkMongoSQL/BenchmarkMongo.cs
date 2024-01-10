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
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;

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

            //streams
            bool streams = false;
            
            if (streams)
            {
                Task.Run(async () =>
                {
                    var collection = mongoDB.GetCollection<Club>("Club");
                    var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<Club>>().Match("{ operationType: { $in: ['insert', 'update', 'delete'] } }");

                    var options = new ChangeStreamOptions { FullDocument = ChangeStreamFullDocumentOption.UpdateLookup };

                    var cursor = await collection.WatchAsync(pipeline, options);

                    using (cursor)
                    {
                        while (await cursor.MoveNextAsync())
                        {
                            foreach (var change in cursor.Current)
                            {
                                // Handle change event
                                var club = change.FullDocument; // Access the changed Club object

                                // Print out the Club object
                                Console.WriteLine($"Change detected in Club: {club}");
                            }
                        }
                    }
                });
            }


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
            //Time for CourtRequest
            Console.WriteLine("Time for CourtRequest: " + stopwatch.ElapsedMilliseconds);

            //End Benchmark
            stopwatch.Stop();
            Console.WriteLine("End Benchmark: " + stopwatch.ElapsedMilliseconds);

            //Delete Database
            // MongoDB doesn't have a direct equivalent to 'EnsureDeleted'. You'll need to drop each collection manually or drop the whole database.
            if (!streams)
            {
                //mongoDB.DropCollection("Clubs");
                //mongoDB.DropCollection("Users");
                //mongoDB.DropCollection("Reservations");
            }

            return new OkObjectResult(JsonSerializer.Serialize(courtRequestDto));
        }

        public CourtRequestDto CourtRequest(IMongoDatabase db)
        {
            var clubsCollection = db.GetCollection<Club>("Clubs");
            var reservationsCollection = db.GetCollection<Reservation>("Reservations");

            Console.WriteLine("Clubs: " + clubsCollection.CountDocuments(_ => true));

            //Make a request to get all courts of a club

            var filter = Builders<Club>.Filter.Eq(club => club.Link, "tceichgraben");
            var club = clubsCollection.Find(filter).FirstOrDefault();

            if (club != null)
            {
                Console.WriteLine("Found Club: " + club.Name);
            }
            else
            {
                Console.WriteLine("Club with link 'tceichgraben' not found.");
            }
            //Club club = clubsCollection.Find(_ => _.Link == "tceichgraben").FirstOrDefault();

            Console.WriteLine("Club: " + JsonSerializer.Serialize(club));

            CourtRequestDto courtRequestDto = new();

            //Map the courts to the CourtRequestDto
            courtRequestDto.ClubName = club.Name;
            courtRequestDto.KW = "KW 1";
            var dayFrom = new DateTime(2023, 12, 11);
            var dayTo = new DateTime(2023, 12, 17);
            courtRequestDto.Courts = new();
            //foreach (Court court in club.Courts)
            //{
            //    CourtDto courtDto = new();
            //    courtDto.Name = court.Name;
            //    courtDto.Days = new();
            //    List<string> days = new() { "Mo", "Di", "Mi", "Do", "Fr", "Sa", "So" };
            //    foreach (var dayOfWeek in days)
            //    {
            //        var dayDto = new CourtDayDto { Name = dayOfWeek, Reservations = new List<ReservationDto>() };

            //        var filter = Builders<Reservation>.Filter
            //            .Where(r => r.CourtNavigation.Id == court.Id && r.StartTime.DayOfWeek.ToString().Substring(0, 2) == dayOfWeek &&
            //                        r.StartTime >= dayFrom && r.StartTime <= dayTo);

            //        var reservations = reservationsCollection.Find(filter).ToList();

            //        Console.WriteLine("Reservations: " + reservations.Count);

            //        foreach (var reservation in reservations)
            //        {
            //            var reservationDto = new ReservationDto
            //            {
            //                From = reservation.StartTime.ToString("HH:mm"),
            //                To = reservation.EndTime.ToString("HH:mm")
            //            };

            //            dayDto.Reservations.Add(reservationDto);
            //        }

            //        courtDto.Days.Add(dayDto);
            //    }
            //    //                for (int i = 0; i < 7; i++)
            //    //                {
            //    //                    CourtDayDto dayDto = new();
            //    //                    dayDto.Name = days[i];
            //    //                    dayDto.Reservations = new();

            //    //                    var pipeline = new BsonDocument[]
            //    //{
            //    //                        BsonDocument.Parse(@"
            //    //                        {
            //    //                            $unwind: '$Courts'
            //    //                        }"),
            //    //                        BsonDocument.Parse(@"
            //    //                        {
            //    //                            $match: {
            //    //                                'Courts._id': ObjectId('" + court.Id + @"')
            //    //                            }
            //    //                        }"),
            //    //                        BsonDocument.Parse(@"
            //    //                        {
            //    //                            $project: {
            //    //                                _id: 1,
            //    //                                StartTime: 1,
            //    //                                EndTime: 1,
            //    //                                Comment: 1,
            //    //                                // Include other fields as needed
            //    //                            }
            //    //                        }"),
            //    //                    };

            //    //                    List<Reservation> reservations = reservationsCollection.Aggregate<Reservation>(pipeline).ToList();

            //    //                    //reservationsCollection.Find(r => r.CourtNavigation == court && ((int)r.StartTime.DayOfWeek) == i && r.StartTime >= dayFrom && r.StartTime <= dayTo).ToList();
            //    //                    for (int j = 0; j < reservations.Count; j++)
            //    //                    {
            //    //                        ReservationDto reservationDto = new();
            //    //                        reservationDto.From = reservations[j].StartTime.ToString("HH:mm");
            //    //                        reservationDto.To = reservations[j].EndTime.ToString("HH:mm");

            //    //                        dayDto.Reservations.Add(reservationDto);
            //    //                    }
            //    //                    courtDto.Days.Add(dayDto);
            //    //                }
            //    courtRequestDto.Courts.Add(courtDto);
            //}

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

                    List<Reservation> reservations = reservationsCollection.Find(r => r.CourtNavigation.Id == court.Id && ((int)r.StartTime.DayOfWeek) == i && r.StartTime >= dayFrom && r.StartTime <= dayTo).ToList();
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

            var options = new CreateIndexOptions { Unique = true };
            //clubsCollection.Indexes.CreateOne("{ Link : 1 }", options);

            var usersCollection = database.GetCollection<User>("Users");
            var reservationsCollection = database.GetCollection<Reservation>("Reservations");

            clubsCollection.DeleteMany(_ => true);
            usersCollection.DeleteMany(_ => true);
            reservationsCollection.DeleteMany(_ => true);

            Club club = CreateClub();
            club.Link = "tceichgraben";

            //Create 10 courts
            for (int i = 0; i < 10; i++)
            {
                Court court = new("Court " + i);
                club.AddCourt(court);
            }

            clubsCollection.InsertOne(club);

            Console.WriteLine(club.Name);
            Console.WriteLine(club.Courts.Count);

            //Console.WriteLine(JsonSerializer.Serialize(club));

            //Get Club
            Club club1 = clubsCollection.Find(_ => true).FirstOrDefault();
            Console.WriteLine(club1.Name);
            Console.WriteLine(club1.Courts.Count);

            //Console.WriteLine(JsonSerializer.Serialize(club1));


            //Create 100 users
            for (int i = 0; i < 100; i++)
            {
                User user = CreateUser();
                usersCollection.InsertOne(user);
            }

            User user1 = CreateUser();
            usersCollection.InsertOne(user1);

            //Create 1000 reservations
            for (int i = 0; i < 1000; i++)
            {
                var user = new MongoDBRef("User", user1.Id);
                var court = new MongoDBRef("Court", club.Courts.FirstOrDefault().Id);
                Reservation reservation = new(DateTime.Now, DateTime.Now.AddHours(1), "", court, user);
                reservationsCollection.InsertOne(reservation);
            }

            //Find reservation
            var filter = Builders<Reservation>.Filter.Eq("CourtNavigation", new MongoDBRef("Court", club.Courts.FirstOrDefault().Id));
            var result = reservationsCollection.Find(filter).ToList();
            Console.WriteLine("Reservations: " + result.Count);

            // Timing
            Stopwatch stopwatch = new();
            stopwatch.Start();

            // 100 Clubs
            for (int i = 0; i < 100; i++)
            {
                Club club2 = CreateClub();
                clubsCollection.InsertOne(club2);
            }

            stopwatch.Stop();
            Console.WriteLine("Time for creating 100 Clubs: " + stopwatch.ElapsedMilliseconds);

            stopwatch.Reset();
            stopwatch.Start();

            // 1000 Clubs
            for (int i = 0; i < 1000; i++)
            {
                Club club2 = CreateClub();
                clubsCollection.InsertOne(club2);
            }

            stopwatch.Stop();
            Console.WriteLine("Time for creating 1000 Clubs: " + stopwatch.ElapsedMilliseconds);

            stopwatch.Reset();
            stopwatch.Start();

            // 100000 Clubs
            for (int i = 0; i < 10000; i++)
            {
                Club club2 = CreateClub();
                clubsCollection.InsertOne(club2);
            }

            stopwatch.Stop();
            Console.WriteLine("Time for creating 100000 Clubs: " + stopwatch.ElapsedMilliseconds);

            // Finds
            var allClubs = clubsCollection.Find(_ => true).ToList();
            Console.WriteLine("All Clubs: " + allClubs.Count);

            var filteredClubs = clubsCollection.Find(c => c.Name == "TC Eichgraben").ToList();
            Console.WriteLine("Filtered Clubs: " + filteredClubs.Count);

            var filteredAndProjectedClubs = clubsCollection
                .Find(c => c.Name == "TC Eichgraben")
                .Project(c => new { c.Name, c.SocialHub })
                .ToList();
            Console.WriteLine("Filtered and projected Clubs: " + filteredAndProjectedClubs.Count);

            var filteredAndSortedClubs = clubsCollection
                .Find(c => c.Name == "TC Eichgraben")
                .SortBy(c => c.Name)
                .Project(c => new { c.Name })
                .ToList();
            Console.WriteLine("Filtered and sorted Clubs: " + filteredAndSortedClubs.Count);

            // Update
            int clubIdToUpdate = 1;

            var filter1 = Builders<Club>.Filter.Eq(c => c.Name, "TC Eichgraben");
            var update = Builders<Club>.Update
                .Set(c => c.Name, "Neuer Clubname")
                .Set(c => c.Address, "Neue Adresse");

            clubsCollection.UpdateOne(filter1, update);

            // Delete
            var clubIdToDelete = clubsCollection.Find(_ => true).FirstOrDefault().Id;

            var deleteFilter = Builders<Club>.Filter.Eq(c => c.Id, clubIdToDelete);
            clubsCollection.DeleteOne(deleteFilter);

            //Some Data for quest fullfillment
            var myclubs = clubsCollection.Find(_ => true).Limit(5).ToList();
            Console.WriteLine("My Clubs: " + myclubs.Count());
            Console.WriteLine("My Clubs: " + JsonSerializer.Serialize(myclubs));

            return database;
        }
    }
}
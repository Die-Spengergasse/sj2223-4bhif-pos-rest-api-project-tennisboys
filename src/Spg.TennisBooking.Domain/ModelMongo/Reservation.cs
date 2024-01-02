using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Spg.TennisBooking.Domain.ModelMongo
{
    public class Reservation
    {
        public ObjectId Id { get; private set; } = ObjectId.GenerateNewId();
        public string UUID { get; private set; } = Guid.NewGuid().ToString();
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        //Foreign Key. Not null
        public MongoDBRef CourtNavigation { get; set; } = default!;
        public MongoDBRef UserNavigation { get; set; } = default!;

        //Additional Info
        public string Comment { get; set; } = string.Empty;

        public Reservation(DateTime startTime, DateTime endTime, string comment, MongoDBRef court, MongoDBRef user)
        {
            StartTime = startTime;
            EndTime = endTime;
            Comment = comment;
            CourtNavigation = court;
            UserNavigation = user;
        }

        protected Reservation()
        {

        }
    }
}
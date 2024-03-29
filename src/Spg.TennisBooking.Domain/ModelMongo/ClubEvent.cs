﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Spg.TennisBooking.Domain.ModelMongo
{
    public class ClubEvent
    {
        public ObjectId Id { get; private set; } = ObjectId.GenerateNewId();
        public string Name { get; set; } = string.Empty;
        public DateTime Time { get; set; }
        public string Info { get; set; } = string.Empty;

        public virtual int ClubNavigationId { get; set; }
        public virtual Club ClubNavigation { get; set; } = default!;

        //Constructer
        public ClubEvent(Club club, string name, DateTime time, string info)
        {
            ClubNavigation = club;
            Name = name;
            Time = time;
            Info = info;
        }

        protected ClubEvent() {

        }
    }
}

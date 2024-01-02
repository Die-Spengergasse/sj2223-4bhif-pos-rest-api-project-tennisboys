using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Spg.TennisBooking.Domain.ModelMongo
{
    public class ClubNews
    {
        public ObjectId Id { get; private set; } = ObjectId.GenerateNewId();
        public string Title { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public DateTime Written { get; set; } = DateTime.UtcNow;
        public virtual int ClubNavigationId { get; set; }
        public virtual Club ClubNavigation { get; set; } = default!;

        //Constructer
        public ClubNews(string title, string info, Club clubNavigation)
        {
            Title = title;
            Info = info;
            ClubNavigation = clubNavigation;
        }
        protected ClubNews() {

        }
    }
}

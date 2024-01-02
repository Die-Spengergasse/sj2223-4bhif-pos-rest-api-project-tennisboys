using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.ModelMongo
{
    public class Club
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public string Link { get; set; } = Guid.NewGuid().ToString(); //Link to the club
        public User? Admin { get; set; }
        public string IBAN { get; set; } = string.Empty;
        public DateTime? PaidTill { get; set; }
        public DateTime FreeTrialTill { get; set; } = DateTime.Now.AddDays(30);
        public string Name { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public SocialHub SocialHub { get; set; } = new SocialHub();
        private List<ClubNews> _clubNews = new List<ClubNews>();
        public IReadOnlyList<ClubNews> ClubNews => _clubNews;

        public void AddClubNews(ClubNews clubNews)
        {
            _clubNews.Add(clubNews);
        }

        public void RemoveClubNews(ClubNews clubNews)
        {
            if(_clubNews.Contains(clubNews))
            {
                _clubNews.Remove(clubNews);
            }
        }
        
        private List<ClubEvent> _clubEvents = new List<ClubEvent>();
        public IReadOnlyList<ClubEvent> ClubEvents => _clubEvents;

        public void AddClubEvent(ClubEvent clubEvent)
        {
            _clubEvents.Add(clubEvent);
        }

        public void RemoveClubEvent(ClubEvent clubEvent)
        {
            if (_clubEvents.Contains(clubEvent))
            {
                _clubEvents.Remove(clubEvent);
            }
        }
        
        private List<Court> _courts = new();
        public IReadOnlyList<Court> Courts => _courts;
        
        public void AddCourt(Court entity)
        {
            if (entity is not null)
            {
                _courts.Add(entity);
            }
        }
        public void RemoveCourt(Court entity)
        {
            if (entity is not null)
            {
                if (_courts.Contains(entity))
                {
                    _courts.Remove(entity);
                }
                else
                {
                    throw new ArgumentException("Entity not found");
                }
            }
        }

        private List<Trainer> _trainers = new();
        public IReadOnlyList<Trainer> Trainers => _trainers;
        public void AddTrainer(Trainer entity)
        {
            if (entity is not null)
            {
                _trainers.Add(entity);
            }
        }
        public void RemoveTrainer(Trainer entity)
        {
            if (entity is not null)
            {
                if (_trainers.Contains(entity))
                {
                    _trainers.Remove(entity);
                }
                else
                {
                    throw new ArgumentException("Entity not found");
                }
            }
        }



        //Constructor
        public Club(string name, User user)
        {
            Name = name;
            Admin = user;
        }

        protected Club()
        {
        }
    }
}

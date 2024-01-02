using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Spg.TennisBooking.Domain.ModelMongo
{
    public class SocialHub
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        private string _facebook { get; set; } = string.Empty;
        public string Facebook
        {
            get => _facebook;
            set
            {
                if (value.Length > 0)
                {
                    _facebook = value;
                }
            }
        }
        private string _instagram { get; set; } = string.Empty;
        public string Instagram
        {
            get => _instagram;
            set
            {
                if (value.Length > 0)
                {
                    _instagram = value;
                }
            }
        }
        private string _twitter { get; set; } = string.Empty;
        public string Twitter
        {
            get => _twitter;
            set
            {
                if (value.Length > 0)
                {
                    _twitter = value;
                }
            }
        }
        private string _youtube { get; set; } = string.Empty;
        public string Youtube
        {
            get => _youtube;
            set
            {
                if (value.Length > 0)
                {
                    _youtube = value;
                }
            }
        }
        private string _linkedIn { get; set; } = string.Empty;
        public string LinkedIn
        {
            get => _linkedIn;
            set
            {
                if (value.Length > 0)
                {
                    _linkedIn = value;
                }
            }
        }
        private string _telephone { get; set; } = string.Empty;
        public string Telephone
        {
            get => _telephone;
            set
            {
                if (value.Length > 0)
                {
                    _telephone = value;
                }
            }
        }
        private string _email { get; set; } = string.Empty;
        public string Email
        {
            get => _email;
            set
            {
                if (value.Length > 0)
                {
                    _email = value;
                }
            }
        }
        private string _website { get; set; } = string.Empty;
        public string Website
        {
            get => _website;
            set
            {
                if (value.Length > 0)
                {
                    _website = value;
                }
            }
        }
        
        public SocialHub()
        {
        }
    }
}

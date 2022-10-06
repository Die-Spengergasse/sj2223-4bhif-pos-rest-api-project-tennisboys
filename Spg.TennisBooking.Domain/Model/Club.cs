using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Model
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public SocialHub SocialHub { get; } = new SocialHub();
        public ClubNews ClubNews { get; set; } = new ClubNews();
        public ClubEvent ClubEvent { get; set; } = new ClubEvent();

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
    }
}

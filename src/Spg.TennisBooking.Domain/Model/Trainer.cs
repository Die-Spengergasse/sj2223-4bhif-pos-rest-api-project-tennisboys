using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Model
{
    public class Trainer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public GenderTypes Gender { get; set; }
        public string Info { get; set; } = String.Empty;
        public int TrainingTime { get; set; } //Arbitrary Number
        public string? ImagePath { get; set; }

        public virtual int ClubNavigationId { get; set; }
        public virtual Club ClubNavigation { get; set; } = default!;

        //Constructer
        public Trainer(Club clubNavigation, string firstName, string lastName, GenderTypes gender, string info, int trainingTime, string? imagePath)
        {
            ClubNavigation = clubNavigation;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Info = info;
            TrainingTime = trainingTime;
            ImagePath = imagePath;
        }   

        protected Trainer() {

        }
    }
}

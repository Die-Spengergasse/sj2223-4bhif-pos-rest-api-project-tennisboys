using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Model
{
    internal class Trainers
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public Boolean Gender { get; set; }
        public string Info { get; set; } = String.Empty;
        public int TrainingTime { get; set; }
    }
}

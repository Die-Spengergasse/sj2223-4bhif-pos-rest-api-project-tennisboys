using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Model
{
    public class Reservation
    {
        public int Id { get; private set; }
        public string UUID { get; private set; } = Guid.NewGuid().ToString();
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        //Foreign Key. Not null
        public virtual int? CourtNavigationId { get; set; }
        public virtual Court? CourtNavigation { get; set; } = default!;
        public virtual int? UserNavigationId { get; set; }
        public virtual User? UserNavigation { get; set; } = default!;

        //Additional Info
        public string? Comment { get; set; }

        public Reservation(DateTime startTime, DateTime endTime, Court court, User user)
        {
            StartTime = startTime;
            EndTime = endTime;
            CourtNavigation = court;
            UserNavigation = user;
        }

        protected Reservation()
        {

        }
    }
}
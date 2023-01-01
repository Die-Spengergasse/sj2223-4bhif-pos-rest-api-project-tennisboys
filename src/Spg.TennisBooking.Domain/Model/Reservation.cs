using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public string UUID { get; set; } = Guid.NewGuid().ToString();
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        /*public virtual int UserNavigationId { get; set; }
        public virtual User UserNavigation { get; set; } = default!;
        public virtual int CourtNavigationId { get; set; }
        public virtual Court CourtNavigation { get; set; } = default!;*/

        public Reservation(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        protected Reservation()
        {

        }
    }
}
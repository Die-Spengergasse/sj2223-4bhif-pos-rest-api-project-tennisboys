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
        public DateTime Date { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public Court ?Court { get; set; }
        public Customer ?Customer { get; set; }
        public virtual int ClubNavigationId { get; set; }
        public virtual Club ClubNavigation { get; set; } = default!;

        public Reservation(Club clubNavigation, DateTime date, int startTime, int endTime, Court court, Customer customer)
        {
            ClubNavigation = clubNavigation;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Court = court;
            Customer = customer;
        }
        
        protected Reservation() {

        }
    }

}

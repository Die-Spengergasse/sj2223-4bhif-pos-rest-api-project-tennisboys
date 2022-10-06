using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Model
{
    internal class Reservations
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public Court Court { get; set; }
        public Customer Customer { get; set; }
        public Club Club { get; set; }
    }
}

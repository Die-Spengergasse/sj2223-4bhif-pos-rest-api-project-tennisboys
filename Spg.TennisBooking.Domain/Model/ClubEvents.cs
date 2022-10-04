using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Model
{
    internal class ClubEvents
    {
        public int Id { get; set; }
        public string EventName { get; set; } = string.Empty;
        public int EventTime { get; set; }
        public string EventInfo { get; set; } = string.Empty;
        
    }
}

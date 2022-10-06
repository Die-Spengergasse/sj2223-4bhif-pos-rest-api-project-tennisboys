using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Model
{
    public class ClubEvent
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Time { get; set; }
        public string Info { get; set; } = string.Empty;
        
    }
}

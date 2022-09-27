using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Model
{
    public class ClubNews
    {
        public int Id { get; set; }
        public string NewsTitle { get; set; } = string.Empty;
        public string NewsInfo { get; set; } = string.Empty;
        public int Date { get; set; }
        
    }
}

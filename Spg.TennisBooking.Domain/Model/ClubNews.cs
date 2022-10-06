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
        public string Title { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public int Date { get; set; }
        
    }
}

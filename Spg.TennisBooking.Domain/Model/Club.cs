using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Model
{
    public class Club
    {
        public int Id { get; set; }
        public string ClubName { get; set; } = string.Empty;
        public string ClubInfo { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        

    }
}

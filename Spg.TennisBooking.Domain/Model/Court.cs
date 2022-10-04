using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Model
{
    public class Court
    {
        public int Id { get; set; }
        public Boolean Occupied { get; set; }
        public string Type { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}

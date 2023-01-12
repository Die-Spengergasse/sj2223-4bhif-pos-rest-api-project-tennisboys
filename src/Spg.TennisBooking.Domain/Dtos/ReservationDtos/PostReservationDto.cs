using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Dtos.ReservationDtos
{
    public class PostReservationDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int CourtId{ get; set; }

        public PostReservationDto()
        {
        }
    }
}

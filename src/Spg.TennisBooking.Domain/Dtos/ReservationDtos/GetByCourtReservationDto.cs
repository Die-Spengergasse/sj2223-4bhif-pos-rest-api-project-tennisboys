using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Dtos.ReservationDtos
{
    public class GetByCourtReservationDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public GetByCourtReservationDto()
        {
        }
    }
}

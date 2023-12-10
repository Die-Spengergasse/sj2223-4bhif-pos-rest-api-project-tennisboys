using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Spg.TennisBooking.BenchmarkMongoSQL.Dtos.CourtRequestDto;

namespace Spg.TennisBooking.BenchmarkMongoSQL.Dtos
{
    public class CourtDayDto
    {
        public string Name { get; set; }
        public List<ReservationDto> Reservations { get; set; }
    }
}

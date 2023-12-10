using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.BenchmarkMongoSQL.Dtos
{
    public class CourtDto
    {
        public string Name { get; set; }
        public List<CourtDayDto> Days { get; set; }
    }
}

using Spg.TennisBooking.Domain.Dtos.HaeteosDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Dtos.ReservationDtos
{
    public class GetReservationDto
    {
        public string UUID { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string? CourtName { get; set; } = string.Empty;
        public string? ClubName { get; set; } = string.Empty;

        //HATEOS Links
        public List<LinkDto> Links = new List<LinkDto>();

        public GetReservationDto()
        {
        }
    }
}

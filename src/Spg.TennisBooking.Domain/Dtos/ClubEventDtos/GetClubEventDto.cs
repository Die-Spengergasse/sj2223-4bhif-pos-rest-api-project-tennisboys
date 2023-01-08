using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Dtos.ClubEventDtos
{
    public record GetClubEventDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Time { get; set; }
        public string Info { get; set; } = string.Empty;
        public string ClubLink { get; set; } = string.Empty;

        public GetClubEventDto()
        {
        }
    }
}

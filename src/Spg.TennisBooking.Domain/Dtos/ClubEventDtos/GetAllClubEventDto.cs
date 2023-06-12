using Spg.TennisBooking.Domain.Dtos.HaeteosDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Dtos.ClubEventDtos
{
    public class GetAllClubEventDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Time { get; set; }

        //HATEOS Links
        public List<LinkDto> Links = new List<LinkDto>();

        public GetAllClubEventDto()
        {
        }
    }
}

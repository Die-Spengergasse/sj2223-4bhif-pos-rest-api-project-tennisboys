using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Dtos.ClubNewsDtos
{
    public class GetAllClubNewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Written { get; set; }

        public GetAllClubNewsDto()
        {
        }
    }
}

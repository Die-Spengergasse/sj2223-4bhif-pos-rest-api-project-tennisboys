using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Dtos.ClubEventDtos
{
    public class PostClubEventDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public string Info { get; set; } = string.Empty;
        [Required]
        public string ClubLink { get; set; } = string.Empty;

        public PostClubEventDto()
        {
        }
    }
}

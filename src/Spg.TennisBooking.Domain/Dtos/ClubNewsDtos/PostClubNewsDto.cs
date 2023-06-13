using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Dtos.ClubNewsDtos
{
    public class PostClubNewsDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Info { get; set; } = string.Empty;
        [Required]
        public string ClubLink { get; set; } = string.Empty;

        public PostClubNewsDto()
        {
        }
    }
}

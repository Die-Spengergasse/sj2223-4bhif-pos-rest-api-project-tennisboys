using Spg.TennisBooking.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Spg.TennisBooking.Domain.Dtos.CourtDtos
{
    public class PostCourtDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string ClubLink { get; set; } = string.Empty;

        //Constructor
        public PostCourtDto()
        {
        }
    }
}
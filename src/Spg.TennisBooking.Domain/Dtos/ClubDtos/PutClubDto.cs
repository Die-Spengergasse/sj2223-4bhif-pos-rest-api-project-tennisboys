using Spg.TennisBooking.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Spg.TennisBooking.Domain.Dtos.ClubDtos
{
    public class PutClubDto
    {
        [Required]
        public string Link { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Info { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string ZipCode { get; set; } = string.Empty;
        [Required]
        public string ImagePath { get; set; } = string.Empty;
        [Required]
        public SocialHubDto SocialHubDto { get; set; } = new SocialHubDto();

        //Constructor
        public PutClubDto()
        {
        }
    }
}
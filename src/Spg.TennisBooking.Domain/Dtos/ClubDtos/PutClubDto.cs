using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Dtos.ClubDtos
{
    public class PutClubDto
    {
        public string Link { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public SocialHubDto SocialHubDto { get; set; } = new SocialHubDto();

        //Constructor
        public PutClubDto()
        {
        }
    }
}
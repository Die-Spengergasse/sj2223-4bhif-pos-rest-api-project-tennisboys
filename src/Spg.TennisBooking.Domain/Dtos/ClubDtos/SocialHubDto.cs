using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Dtos.ClubDtos
{
    public class SocialHubDto
    {
        public string Facebook { get; set; } = string.Empty;
        public string Instagram { get; set; } = string.Empty;
        public string Twitter { get; set; } = string.Empty;
        public string Youtube { get; set; } = string.Empty;
        public string LinkedIn { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;

        //Constructor
        public SocialHubDto()
        {
        }
    }
}
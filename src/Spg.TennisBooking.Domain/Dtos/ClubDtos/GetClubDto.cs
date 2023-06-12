using Spg.TennisBooking.Domain.Dtos.HaeteosDtos;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Dtos.ClubDtos
{
    public class GetClubDto
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; } = false;
        public string Link { get; set; } = string.Empty;
        public DateTime? PaidTill { get; set; } = null;
        public DateTime FreeTrialTill { get; set; } = DateTime.Now;
        public string Name { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public SocialHub SocialHub { get; set; } = new SocialHub();

        //HATEOS Links
        public List<LinkDto> Links = new List<LinkDto>();

        //Constructor
        public GetClubDto()
        {
        }

        public static implicit operator GetClubDto(Club v)
        {
            return new GetClubDto
            {
                Link = v.Link,
                PaidTill = v.PaidTill,
                FreeTrialTill = v.FreeTrialTill,
                Name = v.Name,
                Info = v.Info,
                Address = v.Address,
                ZipCode = v.ZipCode,
                ImagePath = v.ImagePath,
                SocialHub = v.SocialHub
            };
        }
    }
}
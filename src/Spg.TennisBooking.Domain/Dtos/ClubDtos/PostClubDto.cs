using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Dtos.ClubDtos
{
    public record PostClubDto
    {
        public bool IsAdmin { get; set; } = false;
        public string Link { get; set; } = string.Empty;
        public User? Admin { get; set; } = null;
        public DateTime? PaidTill { get; set; } = null;
        public DateTime FreeTrialTill { get; set; } = DateTime.Now;
        public string Name { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public SocialHub SocialHub { get; set; } = new SocialHub();

        //Constructor
        public PostClubDto()
        {
        }

        public static implicit operator PostClubDto(Club v)
        {
            return new PostClubDto
            {
                Link = v.Link,
                Admin = v.Admin,
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
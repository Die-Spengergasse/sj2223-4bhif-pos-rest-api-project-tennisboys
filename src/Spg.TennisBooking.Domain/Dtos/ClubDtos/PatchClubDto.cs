using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Dtos.ClubDtos
{
    public record PatchClubDto
    {
        public string Link { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public SocialHub SocialHub { get; set; } = new SocialHub();

        //Constructor
        public PatchClubDto()
        {
        }

        public static implicit operator PatchClubDto(Club v)
        {
            return new PatchClubDto
            {
                Link = v.Link,
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
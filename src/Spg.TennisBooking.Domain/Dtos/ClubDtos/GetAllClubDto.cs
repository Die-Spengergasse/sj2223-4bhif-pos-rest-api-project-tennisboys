using Spg.TennisBooking.Domain.Dtos.HaeteosDtos;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Dtos.ClubDtos
{
    public class GetAllClubDto
    {
        public int Id { get; set; }
        public string Link { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;

        //HATEOS Links
        public List<LinkDto> Links = new List<LinkDto>();

        //Constructor
        public GetAllClubDto()
        {
        }

        public static implicit operator GetAllClubDto(Club v)
        {
            return new GetAllClubDto
            {
                Id = v.Id,
                Link = v.Link,
                Name = v.Name,
                Info = v.Info,
                Address = v.Address,
                ZipCode = v.ZipCode,
                ImagePath = v.ImagePath,
            };
        }
    }
}
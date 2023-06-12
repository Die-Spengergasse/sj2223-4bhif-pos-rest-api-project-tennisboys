using Spg.TennisBooking.Domain.Dtos.HaeteosDtos;
using Spg.TennisBooking.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Dtos.ClubNewsDtos
{
    public class GetClubNewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public DateTime Written { get; set; }
        public string ClubLink { get; set; } = string.Empty;

        //HATEOS Links
        public List<LinkDto> Links = new List<LinkDto>();

        public GetClubNewsDto()
        {
        }

        public static implicit operator GetClubNewsDto(ClubNews v)
        {
            return new GetClubNewsDto
            {
                Id = v.Id,
                Title = v.Title,
                Info = v.Info,
                Written = v.Written,
                ClubLink = v.ClubNavigation.Link
            };
        }
    }
}

using Spg.TennisBooking.Domain.Dtos.HaeteosDtos;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Dtos.CourtDtos
{
    public class GetCourtDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public CourtType Type { get; set; } = CourtType.Sand;
        public double APrice { get; set; }
        public double? BPrice { get; set; }
        public int ATimeFrom { get; set; }
        public int ATimeTill { get; set; }
        public int AWeekendTimeTill { get; set; }

        //HATEOS Links
        public List<LinkDto> Links = new List<LinkDto>();

        //Constructor
        public GetCourtDto()
        {
        }

        public static implicit operator GetCourtDto(Court v)
        {
            return new GetCourtDto()
            {
                Id = v.Id,
                Name = v.Name,
                Type = v.Type,
                APrice = v.APrice,
                BPrice = v.BPrice,
                ATimeFrom = v.ATimeFrom,
                ATimeTill = v.ATimeTill,
                AWeekendTimeTill = v.AWeekendTimeTill
            };
        }
    }
}
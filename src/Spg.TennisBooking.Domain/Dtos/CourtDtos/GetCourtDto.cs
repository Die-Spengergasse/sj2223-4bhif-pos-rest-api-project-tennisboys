using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Dtos.CourtDtos
{
    public record GetCourtDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public CourtType Type { get; set; } = CourtType.Sand;
        public double APrice { get; set; }
        public double? BPrice { get; set; }
        private int ATimeFrom { get; set; }
        private int ATimeTill { get; set; }
        private int AWeekendTimeTill { get; set; }

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
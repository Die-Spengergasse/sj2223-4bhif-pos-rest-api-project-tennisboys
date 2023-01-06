using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Dtos.CourtDtos
{
    public record PatchCourtDto
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
        public PatchCourtDto()
        {
        }
    }
}

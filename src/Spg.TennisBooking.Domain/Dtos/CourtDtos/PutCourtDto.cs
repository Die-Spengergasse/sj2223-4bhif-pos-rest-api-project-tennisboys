using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Dtos.CourtDtos
{
    public class PutCourtDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public CourtType Type { get; set; } = CourtType.Sand;
        public double APrice { get; set; }
        public double? BPrice { get; set; }
        public int ATimeFrom { get; set; }
        public int ATimeTill { get; set; }
        public int AWeekendTimeTill { get; set; }

        //Constructor
        public PutCourtDto()
        {
        }
    }
}

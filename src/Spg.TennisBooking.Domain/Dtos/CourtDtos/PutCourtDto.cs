using Spg.TennisBooking.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Spg.TennisBooking.Domain.Dtos.CourtDtos
{
    public class PutCourtDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public CourtType Type { get; set; } = CourtType.Sand;
        [Required]
        public double APrice { get; set; }
        [Required]
        public double? BPrice { get; set; }
        [Required]
        public int ATimeFrom { get; set; }
        [Required]
        public int ATimeTill { get; set; }
        [Required]
        public int AWeekendTimeTill { get; set; }

        //Constructor
        public PutCourtDto()
        {
        }
    }
}

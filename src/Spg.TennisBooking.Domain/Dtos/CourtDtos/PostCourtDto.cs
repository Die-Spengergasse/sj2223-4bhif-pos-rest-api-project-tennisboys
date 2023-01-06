using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Dtos.CourtDtos
{
    public record PostCourtDto
    {
        public string Name { get; set; } = string.Empty;
        public string ClubLink { get; set; } = string.Empty;

        //Constructor
        public PostCourtDto()
        {
        }
    }
}
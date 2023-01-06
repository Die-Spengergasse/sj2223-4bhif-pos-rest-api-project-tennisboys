namespace Spg.TennisBooking.Domain.Dtos.CourtDtos
{
    public record CourtDto
    {
        public string Password { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;

        //Constructor
        public CourtDto(string password, string newPassword)
        {
            Password = password;
            NewPassword = newPassword;
        }
    }
}
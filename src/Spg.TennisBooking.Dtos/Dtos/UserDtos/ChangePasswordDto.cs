namespace Spg.TennisBooking.Dtos.Dtos.UserDtos
{
    public record ChangePasswordDto
    {
        public string Password { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;

        //Constructor
        public ChangePasswordDto(string uuid, string password, string newPassword)
        {
            Password = password;
            NewPassword = newPassword;
        }
    }
}
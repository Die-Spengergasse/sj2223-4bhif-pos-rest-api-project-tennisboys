namespace Spg.TennisBooking.Domain.Dtos.UserDtos
{
    public class ChangePasswordDto
    {
        public string Password { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;

        //Constructor
        public ChangePasswordDto(string password, string newPassword)
        {
            Password = password;
            NewPassword = newPassword;
        }
    }
}
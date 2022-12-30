namespace Spg.TennisBooking.Api.Dtos.UserDtos
{
    public record ChangePasswordDto
    {
        public string UUID { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;

        //Constructor
        public ChangePasswordDto(string uuid, string password, string newPassword)
        {
            UUID = uuid;
            Password = password;
            NewPassword = newPassword;
        }
    }
}
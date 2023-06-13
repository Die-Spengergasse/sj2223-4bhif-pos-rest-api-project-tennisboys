using System.ComponentModel.DataAnnotations;

namespace Spg.TennisBooking.Domain.Dtos.UserDtos
{
    public class ChangePasswordDto
    {
        [Required]
        [MinLength(8, ErrorMessage = "Password must have at least 8 digits")]
        public string Password { get; set; } = string.Empty;
        [Required]
        [MinLength(8, ErrorMessage = "New Password must have at least 8 digits")]
        public string NewPassword { get; set; } = string.Empty;

        //Constructor
        public ChangePasswordDto(string password, string newPassword)
        {
            Password = password;
            NewPassword = newPassword;
        }
    }
}
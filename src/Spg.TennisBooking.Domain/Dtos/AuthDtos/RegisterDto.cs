using System.ComponentModel.DataAnnotations;
namespace Spg.TennisBooking.Domain.Dtos.AuthDtos
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(8, ErrorMessage = "Password must have at least 8 digits")]
        public string Password { get; set; } = string.Empty;

        //Constructor
        public RegisterDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
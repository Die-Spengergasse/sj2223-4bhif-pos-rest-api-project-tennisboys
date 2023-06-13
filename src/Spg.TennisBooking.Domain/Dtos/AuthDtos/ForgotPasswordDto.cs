using System.ComponentModel.DataAnnotations;
namespace Spg.TennisBooking.Domain.Dtos.AuthDtos
{
    public class ForgotPasswordDto
    {
        [Required]
        public string Email { get; set; } = String.Empty;

        //Constructor
        public ForgotPasswordDto(string email)
        {
            Email = email;
        }
    }
}
using System.ComponentModel.DataAnnotations;
namespace Spg.TennisBooking.Domain.Dtos.AuthDtos
{
    public class ResetPasswordDto
    {
        [Required]
        public string UUID { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must have at least 8 digits")]
        public string Password { get; set; }
        [Required]
        [StringLength(6, ErrorMessage = "ResetCodee must have 6 digits")]
        public string ResetCode { get; set; }

        //Constructor
        public ResetPasswordDto(string uuid, string password, string resetCode)
        {
            UUID = uuid;
            Password = password;
            ResetCode = resetCode;
        }
    }
}
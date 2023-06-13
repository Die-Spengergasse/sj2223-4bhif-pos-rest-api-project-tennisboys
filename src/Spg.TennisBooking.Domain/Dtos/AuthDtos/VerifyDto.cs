using System.ComponentModel.DataAnnotations;

namespace Spg.TennisBooking.Domain.Dtos.AuthDtos
{
    public class VerifyDto
    {
        [Required(ErrorMessage ="UUID wrong")]
        public string UUID { get; set; } = string.Empty;
        [Required(ErrorMessage = "Verification Code wrong")]
        [StringLength(6, ErrorMessage = "Verification Code must have 6 digits")]
        public string VerificationCode { get; set; } = string.Empty;

        //Constructor        
        public VerifyDto(string uuid, string verificationCode)
        {
            UUID = uuid;
            VerificationCode = verificationCode;
        }
    }
}
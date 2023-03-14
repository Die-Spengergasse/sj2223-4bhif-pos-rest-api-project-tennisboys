namespace Spg.TennisBooking.Domain.Dtos.AuthDtos
{
    public class VerifyDto
    {
        public string UUID { get; set; } = string.Empty;
        public string VerificationCode { get; set; } = string.Empty;

        //Constructor
        public VerifyDto(string uuid, string verificationCode)
        {
            UUID = uuid;
            VerificationCode = verificationCode;
        }
    }
}
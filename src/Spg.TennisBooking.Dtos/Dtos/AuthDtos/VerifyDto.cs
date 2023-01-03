namespace Spg.TennisBooking.Dtos.Dtos.AuthDtos
{
    public record VerifyDto
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
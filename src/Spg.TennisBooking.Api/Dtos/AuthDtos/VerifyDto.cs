namespace Spg.TennisBooking.Api.Dtos.AuthDtos
{
    public record VerifyDto
    {
        public string UUID { get; set; } = string.Empty;
        public string VerificationCode { get; set; } = string.Empty;
    }
}
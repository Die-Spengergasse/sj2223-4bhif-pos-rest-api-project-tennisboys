namespace Spg.TennisBooking.Api.Dtos
{
    public record VerifyDto
    {
        public string UUID { get; set; } = string.Empty;
        public string VerificationCode { get; set; } = string.Empty;
    }
}
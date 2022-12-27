namespace Spg.TennisBooking.Api.Dtos.AuthDtos
{
    public record ResetPasswordDto
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public string ResetCode { get; init; }
    }
}
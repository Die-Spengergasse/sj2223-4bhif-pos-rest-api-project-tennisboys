namespace Spg.TennisBooking.Api.Dtos.AuthDtos
{
    public record ForgotPasswordDto
    {
        public string Email { get; set; } = String.Empty;
    }
}
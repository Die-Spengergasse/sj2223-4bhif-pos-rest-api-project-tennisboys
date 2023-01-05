namespace Spg.TennisBooking.Domain.Dtos.AuthDtos
{
    public record ForgotPasswordDto
    {
        public string Email { get; set; } = String.Empty;

        //Constructor
        public ForgotPasswordDto(string email)
        {
            Email = email;
        }
    }
}
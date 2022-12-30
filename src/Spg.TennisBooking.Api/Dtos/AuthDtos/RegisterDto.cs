namespace Spg.TennisBooking.Api.Dtos.AuthDtos
{
    public record RegisterDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        //Constructor
        public RegisterDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
namespace Spg.TennisBooking.Api.Dtos.AuthDtos
{
    public record LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        //Constructor
        public LoginDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
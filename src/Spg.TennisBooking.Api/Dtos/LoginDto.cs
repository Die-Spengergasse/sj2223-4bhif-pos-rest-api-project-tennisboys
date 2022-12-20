namespace ZID.Automat.Api.Dtos
{
    public record LoginDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
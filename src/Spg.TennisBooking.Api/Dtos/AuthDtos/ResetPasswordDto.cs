namespace Spg.TennisBooking.Api.Dtos.AuthDtos
{
    public record ResetPasswordDto
    {
        public string UUID { get; set; }
        public string Password { get; set; }
        public string ResetCode { get; set; }

        //Constructor
        public ResetPasswordDto(string uuid, string password, string resetCode)
        {
            UUID = uuid;
            Password = password;
            ResetCode = resetCode;
        }
    }
}
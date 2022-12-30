using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Api.Dtos.UserDtos
{
    public record PersonalDataDto
    {
        public string FirstName {get; set;} = string.Empty;
        public string LastName {get; set;} = string.Empty;
        public DateTime? BirthDate {get; set;} = null;
        public GenderTypes Gender {get; set;} = GenderTypes.NotSpecified;
        public PhoneNumber? PhoneNumber {get; set;} = null;

        //Constructor
        public PersonalDataDto(string firstName, string lastName, DateTime? birthDate, GenderTypes gender, PhoneNumber? phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Gender = gender;
            PhoneNumber = phoneNumber;
        }
    }
}
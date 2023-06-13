using Spg.TennisBooking.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Spg.TennisBooking.Domain.Dtos.UserDtos
{
    public class PersonalDataDto
    {
        [Required]
        public string FirstName {get; set;} = string.Empty;
        [Required]
        public string LastName {get; set;} = string.Empty;
        [Required]
        public DateTime? BirthDate {get; set;} = null;
        [Required]
        public GenderTypes Gender {get; set;} = GenderTypes.NotSpecified;
        [Required]
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Model
{
    public enum GenderTypes { Male = 0, Female = 1, Diverse = 2}
    public class Customer
    {
        public int Id { get; private set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public GenderTypes Gender { get; set; }
        public string Address { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public PhoneNumber? PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; } = DateTime.Now;

        public Customer(string firstName, string lastName, GenderTypes gender, string address, string email, PhoneNumber? phoneNumber, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Address = address;
            Email = email;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
        }
        protected Customer() {

        }
    }
}

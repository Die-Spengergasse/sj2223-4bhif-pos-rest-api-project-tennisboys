using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Model
{
    public enum GenderTypes { Male = 0, Female = 1, Diverse = 2, None = 3}
    public class User
    {
        public int Id { get; private set; }
        public string UUID { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set;  } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string VerificationCode { get; set; } = String.Empty;
        public bool Verified { get; set; } = false;
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public GenderTypes Gender { get; set; } = GenderTypes.None;
        public string Address { get; set; } = String.Empty;
        public PhoneNumber? PhoneNumber { get; set; } = null;
        public DateTime? BirthDate { get; set; } = null;
        public DateTime RegistrationDate { get; } = DateTime.Now;
        

        public User(string email, string password, string verificationCode)
        {
            Email = email;
            Password = password;
            VerificationCode = verificationCode;
        }
        protected User() {

        }
    }
}

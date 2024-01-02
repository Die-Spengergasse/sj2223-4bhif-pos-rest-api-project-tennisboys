using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Spg.TennisBooking.Domain.ModelMongo
{
    public enum GenderTypes { Male = 0, Female = 1, Diverse = 2, NotSpecified = 3}
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public string UUID { get; private set; } = Guid.NewGuid().ToString();
        public string Email { get; set;  } = String.Empty;
        public string Password { get; set; } = String.Empty;
        //Verification
        public string VerificationCode { get; set; } = String.Empty;
        public bool Verified { get; set; } = false;
        //Recover Password
        public string ResetCode { get; set; } = String.Empty;
        public DateTime? ResetCodeExpires { get; set; } = DateTime.UtcNow;
        //Personal
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public GenderTypes Gender { get; set; } = GenderTypes.NotSpecified;
        public PhoneNumber? PhoneNumber { get; set; } = null;
        public DateTime? BirthDate { get; set; } = null;
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        //Welcomed
        public bool Welcomed { get; set; } = false;
        //Reservations
        private List<Reservation> _reservations = new List<Reservation>();
        public IReadOnlyList<Reservation> Reservations => _reservations;

        public User(string email, string password, string verificationCode)
        {
            Email = email;
            Password = password;
            VerificationCode = verificationCode;
        }
        protected User() {

        }

        //AddRemove
        public void AddReservation(Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation));
            if (_reservations.Contains(reservation))
                throw new ArgumentException("Reservation already exists");
            _reservations.Add(reservation);
        }
        public void RemoveReservation(Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation));
            if (!_reservations.Contains(reservation))
                throw new ArgumentException("Reservation does not exist");
            _reservations.Remove(reservation);
        }
    }
}

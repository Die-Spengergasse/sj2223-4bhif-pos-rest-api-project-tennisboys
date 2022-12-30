using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IUserService
    {
        public bool Welcomed(string uuid);
        public User GetPersonalData(string uuid);
        public bool SetPersonalData(string uuid, string firstName, string lastName, DateTime? birthDate, GenderTypes gender, PhoneNumber? phoneNumber);
        public bool ChangePassword(string uuid, string password, string newPassword);
    }
}
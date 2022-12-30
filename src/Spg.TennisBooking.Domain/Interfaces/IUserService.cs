using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IUserService
    {
        public bool PersonalData(string uuid, string firstName, string lastName, DateTime? birthDate, GenderTypes gender, PhoneNumber? phoneNumber);
        public bool ChangePassword(string uuid, string password, string newPassword);
    }
}
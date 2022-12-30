using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IUserRepository
    {
        User CreateUser(string email, string password, string verificationCode);
        User? GetUserByEmail(string email);
        User? GetUserByUuid(string uuid);
        bool UpdateUser(User user);
        
    }
}

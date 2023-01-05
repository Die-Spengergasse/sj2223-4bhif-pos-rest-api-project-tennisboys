using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IUserRepository
    {
        User Create(string email, string password, string verificationCode);
        User? GetByEmail(string email);
        User? GetByUUIDold(string uuid);
        Task<User?> GetByUUID(string uuid);
        bool Update(User user);
    }
}

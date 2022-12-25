using Spg.TennisBooking.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IAuthRepository
    {
        //Create new user
        User CreateUser(string email, string password, string verificationCode);

        //Check if user already exists
        User? GetUserByEmail(string email);
        User? GetUserByUuid(string uuid);
        bool UpdateUser(User user);
    }
}

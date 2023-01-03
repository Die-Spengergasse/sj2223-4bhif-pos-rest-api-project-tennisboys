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
        User CreateUser(User user);
        User? GetUserByEmail(string email);
        User? GetUserByUuid(string uuid);
        bool UpdateUser(User user);
    }
}

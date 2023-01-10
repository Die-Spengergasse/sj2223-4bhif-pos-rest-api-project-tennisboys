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
        User Create(User user);
        User? GetByEmail(string email);
        User? GetByUUIDold(string uuid);
        bool Update(User user);
    }
}

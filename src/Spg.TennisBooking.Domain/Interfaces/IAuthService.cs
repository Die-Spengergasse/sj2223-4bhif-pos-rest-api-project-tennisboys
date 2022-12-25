using Spg.TennisBooking.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IAuthService
    {
        string Login(string email, string password);
        bool Logout();
        bool Register(string email, string password);
        User GetCurrentUser();
    }
}

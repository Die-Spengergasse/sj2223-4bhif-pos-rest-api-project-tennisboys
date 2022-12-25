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
        bool EmailInUse(string email);
        User Register(string email, string password);
        bool Verify(string uuid, string verificationCode);
        string Login(string email, string password, string secret);
    }
}

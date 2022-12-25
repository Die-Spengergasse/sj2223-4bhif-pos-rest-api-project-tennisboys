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
       /* //Get user with specific email
        User GetUser(string email);
*/
        //Create new user
        User CreateUser(string email, string password);

        /*//Check if user exists
        bool UserExists(string email);

        //Get Clubs where user is admin
        IEnumerable<Club> GetAdminClubs(string email);*/
    }
}

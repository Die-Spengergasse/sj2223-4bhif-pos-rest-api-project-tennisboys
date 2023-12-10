using Spg.TennisBooking.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.BenchmarkMongoSQL
{
    public class BenchmarkStatic
    {
        protected static Club CreateClub()
        {
            User user = CreateUser();
            Club club = new("TC Eichgraben", user);
            return club;
        }

        protected static User CreateUser()
        {
            User user = new("adrian.schauer@aon.at", "AdminPswd", "012345");
            return user;
        }
    }
}

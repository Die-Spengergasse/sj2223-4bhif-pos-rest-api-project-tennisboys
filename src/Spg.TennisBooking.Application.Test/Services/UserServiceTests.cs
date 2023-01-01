using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;
using System.Net;
using System.Security.Cryptography;
using Xunit;

namespace Spg.TennisBooking.Application.Test.Services
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;

        public UserServiceTests(IUserService userService)
        {
            _userService = userService;
        }

        //Welcomed
        [Fact]
        public void Welcomed()
        {

        }

        //GetPersonalData
        [Fact]
        public void GetPersonalData()
        {

        }

        //SetPersonalData
        [Fact]
        public void SetPersonalData()
        {

        }

        //ChangePassword
        [Fact]
        public void ChangePassword()
        {
            
        }
    }
}

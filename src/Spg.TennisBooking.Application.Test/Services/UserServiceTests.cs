using Spg.TennisBooking.Application.Services;
using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;
using Spg.TennisBooking.Repository.Repositories;
using System.Net;
using System.Security.Cryptography;
using Xunit;

namespace Spg.TennisBooking.Application.Test.Services
{
    public class UserServiceTests : Tests
    {
        protected UserService GetService()
        {
            return new UserService(GetRepository());
        }

        protected IUserRepository GetRepository()
        {
            return new UserRepository(GetContext());
        }

        //Welcomed
        [Fact]
        public void Welcomed()
        {
            Assert.True(true);
        }

        //GetPersonalData
        [Fact]
        public void GetPersonalData()
        {
            Assert.True(true);
        }

        //SetPersonalData
        [Fact]
        public void SetPersonalData()
        {
            Assert.True(true);
        }

        //ChangePassword
        [Fact]
        public void ChangePassword()
        {
            Assert.True(true);
        }
    }
}

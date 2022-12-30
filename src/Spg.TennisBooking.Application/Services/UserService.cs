using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;
using System.Net;
using System.Net.Mail;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace Spg.TennisBooking.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool PersonalData(string uuid, string firstName, string lastName, DateTime? birthDate, GenderTypes gender, PhoneNumber? phoneNumber)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string uuid, string password, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}

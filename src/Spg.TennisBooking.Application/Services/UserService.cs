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

        public bool Welcomed(string uuid)
        {
            //Check if welcomed is checked
            User? user = _userRepository.GetByUUID(uuid);
            if (user == null)
            {
                throw new HttpException("User not found", HttpStatusCode.NotFound);
            }
            return user.Welcomed;
        }

        public User GetPersonalData(string uuid)
        {
            //Get personal data
            User? user = _userRepository.GetByUUID(uuid);
            if (user == null)
            {
                throw new HttpException("User not found", HttpStatusCode.NotFound);
            }
            return user;
        }

        public bool SetPersonalData(string uuid, string firstName, string lastName, DateTime? birthDate, GenderTypes gender, PhoneNumber? phoneNumber)
        {
            User? user = _userRepository.GetByUUID(uuid);
            if (user == null)
            {
                throw new HttpException("User not found", HttpStatusCode.NotFound);
            }

            user.FirstName = firstName;
            user.LastName = lastName;
            user.BirthDate = birthDate;
            user.Gender = gender;
            user.PhoneNumber = phoneNumber;
            user.Welcomed = true;

            return _userRepository.Update(user);
        }

        public bool ChangePassword(string uuid, string password, string newPassword)
        {
            //Get user
            User? user = _userRepository.GetByUUID(uuid);

            //Check if user exists
            if (user == null)
            {
                throw new HttpException("User not found", HttpStatusCode.NotFound);
            }

            //Check Password
            //https://stackoverflow.com/questions/4181198/how-to-hash-a-password
            //Check if password is correct
            /* Fetch the stored value */
            string savedPasswordHash = user.Password;
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    throw new HttpException("Password is incorrect", HttpStatusCode.Forbidden);

            //Check if new password is valid
            if (newPassword.Length < 8)
            {
                throw new HttpException("New Password is too short", HttpStatusCode.BadRequest);
            }

            //Hash new password by calling HashPassword from auth
            string hashedPassword = AuthService.HashPassword(newPassword);

            //Update user
            user.Password = hashedPassword;
            return _userRepository.Update(user);
        }
    }
}

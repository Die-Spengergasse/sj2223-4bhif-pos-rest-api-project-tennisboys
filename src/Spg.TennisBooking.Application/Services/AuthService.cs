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
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public bool EmailInUse(string email)
        {
            return _authRepository.GetUserByEmail(email) != null;
        }

        public User Register(string email, string password)
        {
            //Email valid
            if (!MailAddress.TryCreate(email, out _))
            {
                //throw error
                throw new HttpException("Email is invalid", HttpStatusCode.BadRequest);
            }

            //Check if email is already in use
            if (_authRepository.GetUserByEmail(email) != null)
            {
                //throw error
                throw new HttpException("Email is already in use", HttpStatusCode.Conflict);
            }

            //Create VerificationCode. 6 Numbers only
            string verificationCode = new Random().Next(100000, 999999).ToString();

            //Hash password
            //https://stackoverflow.com/questions/4181198/how-to-hash-a-password
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            //Create User
            User user = _authRepository.CreateUser(email, savedPasswordHash, verificationCode);

            //TODO: Send verification email
            //Create MailMessage


            //Return Success
            return user;
        }

        public bool Verify(string uuid, string verificationCode)
        {
            //Get User by uuid
            User? user = _authRepository.GetUserByUuid(uuid);

            //Check if user exists
            if (user == null)
            {
                //throw error
                throw new HttpException("User not found", HttpStatusCode.NotFound);
            }
            //Check if user is already verified
            if (user.Verified)
            {
                //throw error
                throw new HttpException("User already verified", HttpStatusCode.BadRequest);
            }
            //Check if verificationCode is correct
            if (user.VerificationCode != verificationCode)
            {
                //throw error
                throw new HttpException("VerificationCode is incorrect", HttpStatusCode.BadRequest);
            }

            //Set User verified
            user.Verified = true;

            //Update User
            _authRepository.UpdateUser(user);

            //Return Success
            return true;
        }

        public string Login(string email, string password, string secret)
        {
            /*string role = await CheckUserAndGetRole(credentials);
            if (role == null) { return null; }*/

            //Get User
            User? user = _authRepository.GetUserByEmail(email);

            //Check if user exists
            if (user == null)
            {
                //throw error
                throw new HttpException("User not found", HttpStatusCode.NotFound);
            }

            //Check if user is verified
            if (!user.Verified)
            {
                //throw error
                throw new HttpException("User not verified", HttpStatusCode.BadRequest);
            }

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

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                // Payload für den JWT.
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // Benutzername als Typ ClaimTypes.Name.
                    new Claim(ClaimTypes.Name, user.UUID),
                    // Rolle des Benutzer als ClaimTypes.DefaultRoleClaimType
/*                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
*/                }),
                /*                Expires = DateTime.UtcNow + lifetime,
                */
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

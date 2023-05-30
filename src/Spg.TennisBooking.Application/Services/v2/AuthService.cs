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


namespace Spg.TennisBooking.Application.Services.v2
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool EmailInUse(string email)
        {
            return _userRepository.GetByEmail(email) != null;
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
            User? user = _userRepository.GetByEmail(email);
            if (user != null && user.Verified)
            {
                //throw error
                throw new HttpException("Email is already in use", HttpStatusCode.Conflict);
            }

            //Check if password is valid
            if (password.Length < 8)
            {
                //throw error
                throw new HttpException("Password is too short", HttpStatusCode.BadRequest);
            }

            //Create VerificationCode. 6 Numbers only
            string verificationCode = new Random().Next(100000, 999999).ToString();

            //Hash password
            string savedPasswordHash = HashPassword(password);

            //Check if user already exists
            if (user != null)
            {
                //Update user
                user.VerificationCode = verificationCode;
                user.Password = savedPasswordHash;
                _userRepository.Update(user);
            }
            else
            {
                //Create new user
                user = new User(email, savedPasswordHash, verificationCode);
                _userRepository.Create(user);
            }

            //TODO: Send verification email
            //Create MailMessage


            //Return Success
            return user;
        }

        public bool Verify(string uuid, string verificationCode)
        {
            //Get User by uuid
            User? user = _userRepository.GetByUUIDold(uuid);

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
            _userRepository.Update(user);

            //Return Success
            return true;
        }

        public string Login(string email, string password, string secret)
        {
            /*string role = await CheckUserAndGetRole(credentials);
            if (role == null) { return null; }*/

            //Get User
            User? user = _userRepository.GetByEmail(email);

            //Check if user exists
            if (user == null)
            {
                throw new HttpException("User not found", HttpStatusCode.NotFound);
            }

            //Check if user is verified
            if (!user.Verified)
            {
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

            //lifetime
            //TimeSpan lifetime = TimeSpan.FromDays(7);

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
                //Expires = DateTime.UtcNow + lifetime,

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public User ForgotPassword(string email)
        {
            //Get User
            User? user = _userRepository.GetByEmail(email);

            //Check if user exists
            if (user == null)
            {
                throw new HttpException("User not found", HttpStatusCode.NotFound);
            }

            //Check if user is verified
            if (!user.Verified)
            {
                throw new HttpException("User not verified", HttpStatusCode.BadRequest);
            }

            //Create ResetCode. 6 Numbers only
            string resetCode = new Random().Next(100000, 999999).ToString();

            //Set User resetCode
            user.ResetCode = resetCode;

            //Set till when resetCode is valid
            user.ResetCodeExpires = DateTime.UtcNow.AddMinutes(15);

            //TODO: Send Email. Code is valid for 15 minutes

            //Update User
            _userRepository.Update(user);

            //Return Success
            return user;
        }

        public bool ResetPassword(string uuid, string password, string resetCode)
        {
            //Get User
            User? user = _userRepository.GetByUUIDold(uuid);

            //Check if user exists
            if (user == null)
            {
                throw new HttpException("User not found", HttpStatusCode.NotFound);
            }

            //Check if user is verified
            if (!user.Verified)
            {
                throw new HttpException("User not verified", HttpStatusCode.BadRequest);
            }

            //Check if resetCode is correct
            if (user.ResetCode != resetCode)
            {
                throw new HttpException("ResetCode is incorrect" + user.ResetCode + " " + resetCode, HttpStatusCode.BadRequest);
            }

            //Check if resetCode is still valid
            if (user.ResetCodeExpires < DateTime.UtcNow)
            {
                throw new HttpException("ResetCode is expired", HttpStatusCode.BadRequest);
            }

            //Validate password
            if (password.Length < 8)
            {
                throw new HttpException("Password is too short", HttpStatusCode.BadRequest);
            }

            //Hash new password
            string savedPasswordHash = HashPassword(password);

            //Set User password
            user.Password = savedPasswordHash;

            //Set User resetCode to null
            user.ResetCode = String.Empty;

            //Set User resetCodeExpires to null
            user.ResetCodeExpires = null;

            //Update User
            return _userRepository.Update(user);
        }

       /* public User GetUser(string token)
        {
            //Decode Token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);

            //Get User
            User? user = _authRepository.GetUserByUuid(jwtToken.Subject);

            //Check if user exists
            if (user == null)
            {
                throw new HttpException("User not found", HttpStatusCode.NotFound);
            }

            //Check if user is verified
            if (!user.Verified)
            {
                throw new HttpException("User not verified", HttpStatusCode.BadRequest);
            }

            //Return User
            return user;
        }*/

        public static string HashPassword(string password)
        {
            //https://stackoverflow.com/questions/4181198/how-to-hash-a-password
            byte[] salt;
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }   

        //Not used
        public static string GenerateRandom(int length = 128)
        {
            // Salt erzeugen.
            byte[] salt = new byte[length / 8];
            using (RandomNumberGenerator rnd = RandomNumberGenerator.Create())
            {
                rnd.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
    }
}

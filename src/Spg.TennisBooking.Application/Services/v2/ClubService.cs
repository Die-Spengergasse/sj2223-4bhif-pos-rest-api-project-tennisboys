using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using System.Net;
using System.Net.Mail;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.ClubDtos;
using Spg.TennisBooking.Domain.Model;
using Microsoft.Extensions.Logging;
using Stripe;

namespace Spg.TennisBooking.Application.Services.v2
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISocialHubRepository _socialHubRepository;
        private readonly ILogger<ClubService> _logger;

        public ClubService(IClubRepository clubRepository, IUserRepository userRepository, ISocialHubRepository socialHubRepository, ILogger<ClubService> logger)
        {
            _clubRepository = clubRepository;
            _userRepository = userRepository;
            _socialHubRepository = socialHubRepository;
            _logger = logger;
        }

        /// <summary>
        /// Creates a Club.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="uuid"></param>
        /// <returns>return new CreatedResult("/c/" + club.Link, new { message = "Club created", club.Link });</returns>
        public async Task<IActionResult> Create(string name, string uuid)
        {
            //Get user
            User? user = await _userRepository.GetByUUID(uuid);

            //Check if user exists
            if (user == null)
            {
                _logger.LogWarning("User with uuid {uuid} not found", uuid);
                return new NotFoundObjectResult("User not found");
            }

            Club club = new (name, user);

            //Add club to database
            _clubRepository.Add(club);

            //Return club
            return new CreatedResult("/c/" + club.Link, new { message = "Club created", club.Link });
        }

        /// <summary>
        /// Deletes a Club.
        /// </summary>
        /// <param name="link"></param>
        /// <param name="uuid"></param>
        /// <returns>return new OkObjectResult(new { message = "Club deleted" });</returns>
        public async Task<IActionResult> Delete(string link, string uuid)
        {
            //Get club
            Club? club = await _clubRepository.GetByLink(link);

            //Check if club exists
            if (club == null)
            {
                _logger.LogWarning("Club with link {link} not found", link);
                return new NotFoundObjectResult("Club not found");
            }

            //Get user
            User? user = await _userRepository.GetByUUID(uuid);

            //Check if user exists
            if (user == null)
            {
                _logger.LogWarning("User with uuid {uuid} not found", uuid);
                return new NotFoundObjectResult("User not found");
            }

            //Check if user is owner
            if (club.Admin != user)
            {
                _logger.LogWarning("User with uuid {uuid} is not owner of club with link {link}", uuid, link);
                return new UnauthorizedObjectResult("User is not owner of club");
            }

            //Delete club
            _clubRepository.Delete(club);

            //Return club
            return new OkObjectResult(new { message = "Club deleted" });
        }

        /// <summary>
        /// Gives back a Club.
        /// </summary>
        /// <param name="link"></param>
        /// <param name="uuid"></param>
        /// <returns>return new OkObjectResult(clubDto);</returns>
        public async Task<IActionResult> Get(string link, string uuid)
        {
            _logger.LogInformation("Get club with link {link} and uuid {uuid}", link, uuid);
            Club? club = await _clubRepository.GetByLink(link);
            if (club == null)
            {
                return new NotFoundObjectResult("Club not found");
            }
            //Create ClubDto for Club
            GetClubDto clubDto = club;

            //Check if user is admin of club
            if(await IsAdmin(club, uuid, _userRepository))
            {
                clubDto.IsAdmin = true;
            }
            else
            {
                //Clean out sensitive data from clubDto
                clubDto.PaidTill = null;
                clubDto.FreeTrialTill = DateTime.UtcNow;
            }

            return new OkObjectResult(clubDto);
        }

        /// <summary>
        /// Gives back all Clubs.
        /// </summary>
        /// <param name="search"></param>
        /// <returns>return new OkObjectResult(clubDtos);</returns>
        public async Task<IActionResult> GetAll(string? search)
        {
            _logger.LogInformation("Get all clubs with search {search}", search);

            if (search == null) search = "";

            IEnumerable<Club> clubs = await _clubRepository.GetAll(search);
            List<GetAllClubDto> clubDtos = new();
            foreach (Club club in clubs)
            {
                clubDtos.Add(club);
            }
            return new OkObjectResult(clubDtos);
        }

        /// <summary>
        /// GIves back the PaymentKey
        /// </summary>
        /// <param name="link"></param>
        /// <param name="uuid"></param>
        /// <returns>return new OkObjectResult(new { paymentKey = paymentIntent.ClientSecret });</returns>
        public async Task<IActionResult> GetPayementKey(string link, string uuid)
        {
            //Get Club
            Club? club = await _clubRepository.GetByLink(link);
            if (club == null)
            {
                return new NotFoundObjectResult("Club not found");
            }

            //Check if user is admin of club
            if (!await IsAdmin(club, uuid, _userRepository))
            {
                return new UnauthorizedObjectResult("User is not admin of club");
            }

            //Create Stripe instance
            PaymentIntentService service = new PaymentIntentService();

            /*
             * PHP Example
             * $paymentIntent = $stripe->paymentIntents->create([
             *  'automatic_payment_methods' => ['enabled' => true],
             *  'amount' => 1999,
                'currency' => 'eur',
                'metadata' => [
                    'reservation_id' => '12345',
                ]
            ]);
            */
            //TODO: Configure intent correctly
            PaymentIntent paymentIntent = service.Create(new PaymentIntentCreateOptions
            {
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true
                },
                Amount = 1999,
                Currency = "eur",
                Metadata = new Dictionary<string, string>
                {
                    { "intent", "club" },
                    { "club_id", "12345" }
                }
            });

            return new OkObjectResult(new { paymentKey = paymentIntent.ClientSecret });
        }

        /// <summary>
        /// Returns, if the Club is paid.
        /// </summary>
        /// <param name="link"></param>
        /// <param name="uuid"></param>
        /// <returns>return new OkObjectResult(new { IsPaid = true });</returns>
        public async Task<IActionResult> IsPaid(string link, string uuid)
        {
            //IsPaid can be considered when the club does not have less than a month left of the subscription
            //Get club
            Club? club = await _clubRepository.GetByLink(link);

            //Check if club exists
            if (club == null)
            {
                return new NotFoundObjectResult("Club not found");
            }

            //Check if club is paid
            if (club.PaidTill == null)
            {
                return new OkObjectResult(new { isPaid = false });
            }
            else if (club.PaidTill < DateTime.Now.AddMonths(1))
            {
                return new OkObjectResult(new { IsPaid = false });
            }
            else
            {
                return new OkObjectResult(new { IsPaid = true });
            }
        }

        /// <summary>
        /// Puts the Club.
        /// </summary>
        /// <param name="putClubDto"></param>
        /// <param name="uuid"></param>
        /// <returns>return new OkObjectResult("Club updated");</returns>
        public async Task<IActionResult> Put(PutClubDto putClubDto, string uuid)
        {
            //Get club
            Club? club = await _clubRepository.GetByLink(putClubDto.Link);

            //Check if club exists
            if (club == null)
            {
                return new NotFoundObjectResult("Club not found");
            }

            //Check if user is admin of club
            if (!await IsAdmin(club, uuid, _userRepository))
            {
                return new UnauthorizedObjectResult("User is not admin of club");
            }

            //TODO: Validations

            //Update club
            /*
                Link = v.Link,
                Name = v.Name,
                Info = v.Info,
                Address = v.Address,
                ZipCode = v.ZipCode,
                ImagePath = v.ImagePath,
                SocialHub = v.SocialHub
            */
            
            club.Link = putClubDto.Link;
            club.Name = putClubDto.Name;
            club.Info = putClubDto.Info;
            club.Address = putClubDto.Address;
            club.ZipCode = putClubDto.ZipCode;
            club.ImagePath = putClubDto.ImagePath;
            
            club.SocialHub.Facebook = putClubDto.SocialHubDto.Facebook;
            club.SocialHub.Instagram = putClubDto.SocialHubDto.Instagram;
            club.SocialHub.Twitter = putClubDto.SocialHubDto.Twitter;
            club.SocialHub.Youtube = putClubDto.SocialHubDto.Youtube;
            club.SocialHub.LinkedIn = putClubDto.SocialHubDto.LinkedIn;
            club.SocialHub.Telephone = putClubDto.SocialHubDto.Telephone;
            club.SocialHub.Email = putClubDto.SocialHubDto.Email;
            club.SocialHub.Website = putClubDto.SocialHubDto.Website;

            SocialHub socialHub = club.SocialHub;

            //Save changes
            _socialHubRepository.Update(socialHub);
            _clubRepository.Update(club);

            return new OkObjectResult("Club updated");
        }

        /// <summary>
        /// Checks, if User is Admin of the Club.
        /// </summary>
        /// <param name="club"></param>
        /// <param name="uuid"></param>
        /// <param name="userRepository"></param>
        /// <returns>Boolean</returns>
        public static async Task<bool> IsAdmin(Club club, string uuid, IUserRepository userRepository)
        {
            User? user = await userRepository.GetByUUID(uuid);

            if (user == null)
            {
                return false;
            }

            if (club.Admin == user)
            {
                return true;
            }

            return false;
        }
    }
}

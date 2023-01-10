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

namespace Spg.TennisBooking.Application.Services
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<ClubService> _logger;

        public ClubService(IClubRepository clubRepository, IUserRepository userRepository, ILogger<ClubService> logger)
        {
            _clubRepository = clubRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

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

        public async Task<IActionResult> GetAll(string search)
        {
            _logger.LogInformation("Get all clubs with search {search}", search);
            IEnumerable<Club> clubs = await _clubRepository.GetAll(search);
            List<GetAllClubDto> clubDtos = new();
            foreach (Club club in clubs)
            {
                clubDtos.Add(club);
            }
            return new OkObjectResult(clubDtos);
        }
        
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
            if (club.PaidTill < DateTime.Now.AddMonths(1))
            {
                return new OkObjectResult(new { IsPaid = false });
            }
            else
            {
                return new OkObjectResult(new { IsPaid = true });
            }
        }

        public async Task<IActionResult> Put(PatchClubDto patchClubDto, string uuid)
        {
            //Get club
            Club? club = await _clubRepository.GetByLink(patchClubDto.Link);

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
            club.Link = patchClubDto.Link;
            club.Name = patchClubDto.Name;
            club.Info = patchClubDto.Info;
            club.Address = patchClubDto.Address;
            club.ZipCode = patchClubDto.ZipCode;
            club.ImagePath = patchClubDto.ImagePath;
            club.SocialHub = patchClubDto.SocialHub;

            //Save changes
            _clubRepository.Update(club);

            return new OkObjectResult("Club updated");
        }

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

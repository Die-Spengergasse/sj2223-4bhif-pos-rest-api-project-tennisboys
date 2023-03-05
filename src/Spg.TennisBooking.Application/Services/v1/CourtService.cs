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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.CourtDtos;
using Microsoft.Extensions.Configuration;

namespace Spg.TennisBooking.Application.Services
{
    public class CourtService : ICourtService
    {
        private readonly ICourtRepository _courtRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClubRepository _clubRepository;
        private readonly ILogger<CourtService> _logger;
        private readonly IConfiguration _configuration;

        public CourtService(ICourtRepository courtRepository, IUserRepository userRepository, IClubRepository clubRepository, ILogger<CourtService> logger, IConfiguration configuration)
        {
            _courtRepository = courtRepository;
            _userRepository = userRepository;
            _clubRepository = clubRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IActionResult> Delete(int id, string uuid)
        {
            Court? court = await _courtRepository.Get(id);

            if (court == null)
            {
                return new NotFoundObjectResult("Court not found");
            }

            Club club = court.ClubNavigation;

            //Check if user is Admin of Club
            if (!await ClubService.IsAdmin(club, uuid, _userRepository))
            {
                return new UnauthorizedObjectResult("User not owner of this club");
            }

            //Remove
            _courtRepository.Delete(court);

            return new OkObjectResult("Court deleted");
        }

        public async Task<IActionResult> Get(int id)
        {
            Court? court = await _courtRepository.Get(id);

            if (court == null)
            {
                return new NotFoundObjectResult("Court not found");
            }

            //Transform into CourtDto
            GetCourtDto courtDto = court;

            return new OkObjectResult(courtDto);
        }
        
        public async Task<IActionResult> GetAll(string clubLink)
        {
            //Get Club
            Club? club = await _clubRepository.GetByLink(clubLink);

            if (club == null)
            {
                return new NotFoundObjectResult("Club not found");
            }

            //Get all courts
            IEnumerable<Court> courts = await _courtRepository.GetAll(club);

            //Transform into CourtDto
            List<GetCourtDto> courtDtos = courts.Select(court => (GetCourtDto)court).ToList();

            return new OkObjectResult(courtDtos);
        }

        public async Task<IActionResult> Put(PutCourtDto court, string uuid)
        {
            //Get Club of court by getting the court with the help of the id and accessing ClubNavigation
            Court? oldCourt = await _courtRepository.Get(court.Id);

            if(oldCourt == null)
            {
                return new NotFoundObjectResult("Court not found");
            }

            Club club = oldCourt.ClubNavigation;

            //Check if user is Admin of Club
            if (!await ClubService.IsAdmin(club, uuid, _userRepository)){
                return new UnauthorizedObjectResult("User not owner of this club");
            }

            //Patch
            //TODO: Validations and overgive it to oldCourt


            //Save new court
            _courtRepository.Update(oldCourt);

            return new OkObjectResult("Court updated");
        }
        
        public async Task<IActionResult> Post(PostCourtDto postCourtDto, string uuid)
        {
            //Get Club
            Club? club = await _clubRepository.GetByLink(postCourtDto.ClubLink);

            if(club == null)
            {
                return new NotFoundObjectResult("Club not found");
            }

            //Check if admin
            if(!await ClubService.IsAdmin(club, uuid, _userRepository))
            {
                return new UnauthorizedObjectResult("User not owner of this club");
            }

            //Create new Court
            Court court = new(postCourtDto.Name, club);

            _courtRepository.Add(court);

            //Create location
            string url = _configuration.GetSection("MvcFrontEnd").Value;
            Uri uri = new(url + "/c/" + postCourtDto.ClubLink + "/court/" + court.Id+"/edit");

            return new CreatedResult(uri.AbsolutePath, "Court created");
        }
    }
}

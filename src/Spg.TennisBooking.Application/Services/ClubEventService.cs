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
using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.ClubEventDtos;
using Microsoft.Extensions.Configuration;

namespace Spg.TennisBooking.Application.Services
{
    public class ClubEventService : IClubEventService
    {
        private readonly IClubEventRepository _clubEventRepository;
        private readonly IClubRepository _clubRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public ClubEventService(IClubEventRepository clubEventRepository, IClubRepository clubRepository, IUserRepository userRepository, IConfiguration configuration)
        {
            _clubEventRepository = clubEventRepository;
            _clubRepository = clubRepository;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<IActionResult> Get(int id)
        {
            ClubEvent? clubEvent = await _clubEventRepository.Get(id);

            if (clubEvent == null)
            {
                return new NotFoundObjectResult("ClubEvent not found");
            }

            GetClubEventDto getClubEventDto = clubEvent;
            getClubEventDto.ClubLink = clubEvent.ClubNavigation.Link;

            return new OkObjectResult(getClubEventDto);
        }

        public async Task<IActionResult> GetAll(string clubLink)
        {
            //Get Club
            Club? club = await _clubRepository.GetByLink(clubLink);

            if (club == null)
            {
                return new NotFoundObjectResult("Club not found");
            }

            IEnumerable<ClubEvent> clubEvents = await _clubEventRepository.GetAll(club);

            if (clubEvents == null)
            {
                return new NotFoundObjectResult("ClubEvents not found");
            }

            List<GetClubEventDto> getClubEventDtos = new List<GetClubEventDto>();

            foreach (ClubEvent clubEvent in clubEvents)
            {
                GetClubEventDto getClubEventDto = clubEvent;
                getClubEventDto.ClubLink = clubEvent.ClubNavigation.Link;
                getClubEventDtos.Add(getClubEventDto);
            }

            return new OkObjectResult(getClubEventDtos);
        }

        public async Task<IActionResult> Post(PostClubEventDto postClubEventDto, string uuid)
        {
            //Get Club
            Club? club = await _clubRepository.GetByLink(postClubEventDto.ClubLink);

            if (club == null)
            {
                return new NotFoundObjectResult("Club not found");
            }

            //Check if user is admin
            if (!await ClubService.IsAdmin(club, uuid, _userRepository))
            {
                return new UnauthorizedObjectResult("User is not admin");
            }

            //Create ClubEvent
            ClubEvent clubEvent = new ClubEvent(club, postClubEventDto.Name, postClubEventDto.Time, postClubEventDto.Info);

            //Add ClubEvent
            _clubEventRepository.Add(clubEvent);

            //Create location
            string url = _configuration.GetSection("MvcFrontEnd").Value;
            Uri uri = new(url + "/c/" + club.Link + "/events/" + clubEvent.Id);

            return new CreatedResult(uri.AbsoluteUri, clubEvent);
        }

        public async Task<IActionResult> Put(PutClubEventDto putClubEventDto, string uuid)
        {
            //Get ClubEvent
            ClubEvent? clubEvent = await _clubEventRepository.Get(putClubEventDto.Id);

            if (clubEvent == null)
            {
                return new NotFoundObjectResult("ClubEvent not found");
            }

            //Get Club
            Club club = clubEvent.ClubNavigation;

            //Check if user is admin
            if (!await ClubService.IsAdmin(club, uuid, _userRepository))
            {
                return new UnauthorizedObjectResult("User is not admin");
            }

            //Update ClubEvent
            clubEvent.Name = putClubEventDto.Name;
            clubEvent.Time = putClubEventDto.Time;
            clubEvent.Info = putClubEventDto.Info;

            //Update ClubEvent
            _clubEventRepository.Update(clubEvent);

            return new OkObjectResult("ClubEvent updated");
        }

        public async Task<IActionResult> Delete(int id, string uuid)
        {
            //Get ClubEvent
            ClubEvent? clubEvent = await _clubEventRepository.Get(id);

            if (clubEvent == null)
            {
                return new NotFoundObjectResult("ClubEvent not found");
            }

            //Get Club
            Club club = clubEvent.ClubNavigation;

            //Check if user is admin
            if (!await ClubService.IsAdmin(club, uuid, _userRepository))
            {
                return new UnauthorizedObjectResult("User is not admin");
            }

            //Delete ClubEvent
            _clubEventRepository.Delete(clubEvent);

            return new OkObjectResult("ClubEvent deleted");
        }
    }
}

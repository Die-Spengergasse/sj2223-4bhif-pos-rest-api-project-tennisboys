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
using Spg.TennisBooking.Domain.Dtos.HaeteosDtos;

namespace Spg.TennisBooking.Application.Services.v2
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

        /// <summary>
        /// Gives back a ClubEvent.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return new OkObjectResult(getClubEventDto);</returns>
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

        /// <summary>
        /// Gives back all ClubEvents of a Club.
        /// </summary>
        /// <param name="clubLink"></param>
        /// <returns>return new OkObjectResult(getClubEventDtos);</returns>
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
                getClubEventDtos.Add(CreateLinksForClubEvent(getClubEventDto));
            }

            return new OkObjectResult(getClubEventDtos);
        }

        /// <summary>
        /// Posts ClubEvents.
        /// </summary>
        /// <param name="postClubEventDto"></param>
        /// <param name="uuid"></param>
        /// <returns>return new CreatedResult(uri.AbsoluteUri, getClubEventDto);</returns>
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
            //club.AddClubEvent(clubEvent);
            //_clubRepository.Update(club);
            _clubEventRepository.Add(clubEvent);

            //Create location
            string url = _configuration.GetSection("MvcFrontEnd").Value;
            Uri uri = new(url + "/c/" + club.Link + "/events/" + clubEvent.Id);

            //Create DTO
            GetClubEventDto getClubEventDto = clubEvent;

            return new CreatedResult(uri.AbsoluteUri, CreateLinksForClubEvent(getClubEventDto));
        }

        /// <summary>
        /// Puts ClubEvents.
        /// </summary>
        /// <param name="putClubEventDto"></param>
        /// <param name="uuid"></param>
        /// <returns>return new OkObjectResult("ClubEvent updated");</returns>
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

            GetClubEventDto getClubEventDto = clubEvent;

            return new OkObjectResult(CreateLinksForClubEvent(getClubEventDto));
        }

        /// <summary>
        /// Deletes a ClubEvent.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="uuid"></param>
        /// <returns>return new OkObjectResult("ClubEvent deleted");</returns>
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

        private GetClubEventDto CreateLinksForClubEvent(GetClubEventDto clubEventDto)
        {
            clubEventDto.Links.Add(new LinkDto(
                "/api/v2/ClubEventController/" + clubEventDto.Id,
                "self",
                "GET"
            ));

            clubEventDto.Links.Add(new LinkDto(
                "/api/v2/ClubEventController/" + clubEventDto.Id,
                "update",
                "PUT"
            ));

            clubEventDto.Links.Add(new LinkDto(
                "/api/v2/ClubEventController/" + clubEventDto.Id,
                "delete",
                "DELETE"
            ));

            return clubEventDto;
        }
    }
}

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
using Spg.TennisBooking.Domain.Dtos.ClubNewsDtos;
using Microsoft.Extensions.Configuration;

namespace Spg.TennisBooking.Application.Services
{
    public class ClubNewsService : IClubNewsService
    {
        private readonly IClubNewsRepository _clubNewsRepository;
        private readonly IClubRepository _clubRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;


        public ClubNewsService(IClubNewsRepository clubNewsRepository, IClubRepository clubRepository, IUserRepository userRepository, IConfiguration configuration)
        {
            _clubNewsRepository = clubNewsRepository;
            _clubRepository = clubRepository;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<IActionResult> Get(int id)
        {
            ClubNews? clubNews = await _clubNewsRepository.Get(id);

            if (clubNews == null)
            {
                return new NotFoundObjectResult("ClubNews not found");
            }

            return new OkObjectResult(clubNews);
        }
        
        public async Task<IActionResult> GetAll(string clubLink)
        {
            //Get Club
            Club? club = await _clubRepository.GetByLink(clubLink);

            if (club == null)
            {
                return new NotFoundObjectResult("Club not found");
            }
            
            IEnumerable<ClubNews> clubNews = await _clubNewsRepository.GetAll(club);

            if (clubNews == null)
            {
                return new NotFoundObjectResult("ClubNews not found");
            }

            return new OkObjectResult(clubNews);
        }

        public async Task<IActionResult> Post(PostClubNewsDto postClubEventDto, string uuid)
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

            //Create ClubNews
            ClubNews clubNews = new ClubNews(postClubEventDto.Title, postClubEventDto.Info, club);

            //Add ClubNews
            club.AddClubNews(clubNews);
            _clubRepository.Update(club);

            //Create location
            string url = _configuration.GetSection("MvcFrontEnd").Value;
            Uri uri = new(url + "/c/" + club.Link + "/news/" + clubNews.Id);

            return new CreatedResult(uri.AbsoluteUri, clubNews);
        }

        public async Task<IActionResult> Put(PutClubNewsDto putClubEventDto, string uuid)
        {
            //Get ClubNews
            ClubNews? clubNews = await _clubNewsRepository.Get(putClubEventDto.Id);

            if (clubNews == null)
            {
                return new NotFoundObjectResult("ClubNews not found");
            }

            //Get Club
            Club? club = clubNews.ClubNavigation;

            if (club == null)
            {
                return new NotFoundObjectResult("Club not found");
            }

            //Check if user is admin
            if (!await ClubService.IsAdmin(club, uuid, _userRepository))
            {
                return new UnauthorizedObjectResult("User is not admin");
            }

            //Update ClubNews
            clubNews.Title = putClubEventDto.Title;
            clubNews.Info = putClubEventDto.Info;

            //Update ClubNews
            _clubNewsRepository.Update(clubNews);

            return new OkObjectResult(clubNews);
        }

        public async Task<IActionResult> Delete(int id, string uuid)
        {
            //Get ClubNews
            ClubNews? clubNews = await _clubNewsRepository.Get(id);

            if (clubNews == null)
            {
                return new NotFoundObjectResult("ClubNews not found");
            }

            //Get Club
            Club? club = clubNews.ClubNavigation;

            if (club == null)
            {
                return new NotFoundObjectResult("Club not found");
            }

            //Check if user is admin
            if (!await ClubService.IsAdmin(club, uuid, _userRepository))
            {
                return new UnauthorizedObjectResult("User is not admin");
            }

            //Delete ClubNews
            _clubNewsRepository.Delete(clubNews);

            return new OkObjectResult("ClubNews deleted");
        }
    }
}

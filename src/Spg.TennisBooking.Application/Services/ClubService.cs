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

        public Task<IActionResult> Create(string name, string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Delete(string link, string uuid)
        {
            throw new NotImplementedException();
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
            User? user = await _userRepository.GetByUUID(uuid);

            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            if (club.Admin == user)
            {
                clubDto.IsAdmin = true;
            }

            return new OkObjectResult(clubDto);
        }

        public Task<IActionResult> GetPayementKey(string link, string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> IsPaid(string link, string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Patch(PatchClubDto patchClubDto, string uuid)
        {
            throw new NotImplementedException();
        }
    }
}

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

namespace Spg.TennisBooking.Application.Services
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepository;

        public ClubService(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }

        public async Task<IActionResult> Get(string link)
        {
            Club? club = await _clubRepository.GetByLink(link);
            if (club == null)
            {
                return new NotFoundObjectResult("Club not found");
            }
            return new OkObjectResult(club);
        }
    }
}

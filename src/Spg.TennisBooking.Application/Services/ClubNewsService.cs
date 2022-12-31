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
    public class ClubNewsService : IClubNewsService
    {
        private readonly IClubNewsRepository _clubNewsRepository;

        public ClubNewsService(IClubNewsRepository clubNewsRepository)
        {
            _clubNewsRepository = clubNewsRepository;
        }
    }
}

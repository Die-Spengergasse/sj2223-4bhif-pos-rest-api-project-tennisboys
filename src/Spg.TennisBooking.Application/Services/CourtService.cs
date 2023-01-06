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

namespace Spg.TennisBooking.Application.Services
{
    public class CourtService : ICourtService
    {
        private readonly ICourtRepository _courtRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<CourtService> _logger;

        public CourtService(ICourtRepository courtRepository, IUserRepository userRepository, ILogger<CourtService> logger)
        {
            _courtRepository = courtRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public Task<IActionResult> Delete(int id, string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetAll(string clubLink)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Patch(CourtDto court, string uuid)
        {
            throw new NotImplementedException();
        }
        
        public Task<IActionResult> Post(CourtDto court, string uuid)
        {
            throw new NotImplementedException();
        }
    }
}

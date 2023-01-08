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

namespace Spg.TennisBooking.Application.Services
{
    public class ClubEventService : IClubEventService
    {
        private readonly IClubEventRepository _clubEventRepository;

        public ClubEventService(IClubEventRepository clubEventRepository)
        {
            _clubEventRepository = clubEventRepository;
        }

        public Task<IActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetAll(string clubLink)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Post(PostClubEventDto postClubEventDto, string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Put(PutClubEventDto putClubEventDto, string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Delete(int id, string uuid)
        {
            throw new NotImplementedException();
        }
    }
}

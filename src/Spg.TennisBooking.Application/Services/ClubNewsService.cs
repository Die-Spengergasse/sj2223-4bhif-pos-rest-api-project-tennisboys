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

namespace Spg.TennisBooking.Application.Services
{
    public class ClubNewsService : IClubNewsService
    {
        private readonly IClubNewsRepository _clubNewsRepository;

        public ClubNewsService(IClubNewsRepository clubNewsRepository)
        {
            _clubNewsRepository = clubNewsRepository;
        }

        public Task<IActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetAll(string clubLink)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Post(PostClubNewsDto postClubEventDto, string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Put(PutClubNewsDto putClubEventDto, string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Delete(int id, string uuid)
        {
            throw new NotImplementedException();
        }
    }
}

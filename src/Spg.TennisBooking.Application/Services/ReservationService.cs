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
using Spg.TennisBooking.Domain.Dtos.ReservationDtos;

namespace Spg.TennisBooking.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public Task<IActionResult> GetById(int id, string uuid)
        {
            throw new NotImplementedException();
        }
        public Task<IActionResult> GetByClub(string clubLink, string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetByCourt(int courtId)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetByUser(string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Post(PostReservationDto reservation, string uuid)
        {
            throw new NotImplementedException();
        }
        
        public Task<IActionResult> Delete(int id, string uuid)
        {
            throw new NotImplementedException();
        }
    }
}

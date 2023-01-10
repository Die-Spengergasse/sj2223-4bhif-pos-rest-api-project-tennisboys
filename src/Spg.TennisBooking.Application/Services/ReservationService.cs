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
        private readonly IUserRepository _userRepository;

        public ReservationService(IReservationRepository reservationRepository, IUserRepository userRepository)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> GetByUUID(string reservationUUID, string uuid)
        {
            //Get the reservation by UUID
            Reservation? reservation = await _reservationRepository.GetByUUID(reservationUUID);

            //Check if the reservation exists
            if (reservation == null)
            {
                return new NotFoundObjectResult("Reservation not found");
            }

            //Get User
            User? user = await _userRepository.GetByUUID(uuid);

            //Check if the user exists
            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }
            
            //Check if User is allowed to see the reservation
            //The user is allowed when A: The user is the owner of the club, B: The user is the owner of the reservation
            if (reservation.ClubNavigation?.Admin != user && reservation.UserNavigation != user)
            {
                return new UnauthorizedObjectResult("You are not allowed to see this reservation");
            }

            //Put into DTO
            GetReservationDto getReservationDto = new GetReservationDto()
            {
                UUID = reservation.UUID,
                StartTime = reservation.StartTime,
                EndTime = reservation.EndTime,
                CourtName = reservation.CourtNavigation?.Name,
                ClubName = reservation.ClubNavigation?.Name,
            };

            //Return dto
            return new OkObjectResult(getReservationDto);
        }
        public async Task<IActionResult> GetByClub(string clubLink, string uuid)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> GetByCourt(int courtId)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> GetByUser(string uuid)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Post(PostReservationDto reservation, string uuid)
        {
            throw new NotImplementedException();
        }
        
        public async Task<IActionResult> Delete(string reservationUUID, string uuid)
        {
            throw new NotImplementedException();
        }
    }
}

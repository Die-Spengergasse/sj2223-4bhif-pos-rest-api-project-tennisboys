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
using System;
using Microsoft.Extensions.Configuration;

namespace Spg.TennisBooking.Application.Services.v1
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClubRepository _clubRepository;
        private readonly ICourtRepository _courtRepository;
        private readonly IConfiguration _configuration;

        public ReservationService(IReservationRepository reservationRepository, IUserRepository userRepository, IClubRepository clubRepository, ICourtRepository courtRepository, IConfiguration configuration)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _clubRepository = clubRepository;
            _courtRepository = courtRepository;
            _configuration = configuration;
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
                Comment = reservation.Comment,
                CourtName = reservation.CourtNavigation?.Name,
                ClubName = reservation.ClubNavigation?.Name,
            };

            //Return dto
            return new OkObjectResult(getReservationDto);
        }

        public async Task<IActionResult> GetByClub(string clubLink, string uuid)
        {
            //Get the club by link
            Club? club = await _clubRepository.GetByLink(clubLink);

            //Check if the club exists
            if (club == null)
            {
                return new NotFoundObjectResult("Club not found");
            }

            //Get User
            User? user = await _userRepository.GetByUUID(uuid);

            //Check if the user exists
            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            //Check if User is allowed to see the reservations
            //The user is allowed when A: The user is the owner of the club
            if (club.Admin != user)
            {
                return new UnauthorizedObjectResult("You are not allowed to see this reservations");
            }

            //Get all reservations
            IEnumerable<Reservation> reservations = await _reservationRepository.GetByClub(club);

            //Put into DTO
            List<GetReservationDto> getReservationDtos = new List<GetReservationDto>();
            foreach (Reservation reservation in reservations)
            {
                getReservationDtos.Add(new GetReservationDto()
                {
                    UUID = reservation.UUID,
                    StartTime = reservation.StartTime,
                    EndTime = reservation.EndTime,
                    CourtName = reservation.CourtNavigation?.Name,
                    ClubName = reservation.ClubNavigation?.Name,
                });
            }

            //Return dto
            return new OkObjectResult(getReservationDtos);
        }

        public async Task<IActionResult> GetByCourt(int courtId)
        {
            //Get the court by id
            Court? court = await _courtRepository.Get(courtId);

            //Check if the court exists
            if (court == null)
            {
                return new NotFoundObjectResult("Court not found");
            }

            //Get all reservations
            IEnumerable<Reservation> reservations = await _reservationRepository.GetByCourtAndDateRange(court, DateTime.UtcNow - TimeSpan.FromDays(7), DateTime.UtcNow);

            //Put into DTO
            List<GetByCourtReservationDto> getByCourtReservationDtos = new List<GetByCourtReservationDto>();
            foreach (Reservation reservation in reservations)
            {
                getByCourtReservationDtos.Add(new GetByCourtReservationDto()
                {
                    StartTime = reservation.StartTime,
                    EndTime = reservation.EndTime
                });
            }

            //Return dto
            return new OkObjectResult(getByCourtReservationDtos);

        }

        public async Task<IActionResult> GetByUser(string uuid)
        {
            //Get User
            User? user = await _userRepository.GetByUUID(uuid);

            //Check if the user exists
            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            //Get all reservations
            IEnumerable<Reservation> reservations = await _reservationRepository.GetByUser(user);

            //Put into DTO
            List<GetReservationDto> getReservationDtos = new List<GetReservationDto>();
            foreach (Reservation reservation in reservations)
            {
                getReservationDtos.Add(new GetReservationDto()
                {
                    UUID = reservation.UUID,
                    StartTime = reservation.StartTime,
                    EndTime = reservation.EndTime,
                    Comment = reservation.Comment,
                    CourtName = reservation.CourtNavigation?.Name,
                    ClubName = reservation.ClubNavigation?.Name,
                });
            }

            //Return dto
            return new OkObjectResult(getReservationDtos);
        }

        public async Task<IActionResult> Post(PostReservationDto reservation, string uuid)
        {
            //Get User
            User? user = await _userRepository.GetByUUID(uuid);

            //Check if the user exists
            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            //Get the court by id
            Court? court = await _courtRepository.Get(reservation.CourtId);

            //Check if the court exists
            if (court == null)
            {
                return new NotFoundObjectResult("Court not found");
            }

            //Check if the reservation is in the future
            if (reservation.StartTime < DateTime.UtcNow)
            {
                return new BadRequestObjectResult("The reservation is in the past");
            }

            //Check if the reservation is not longer than 2 hours
            if (reservation.EndTime - reservation.StartTime > TimeSpan.FromHours(2))
            {
                return new BadRequestObjectResult("The reservation is longer than 2 hours");
            }

            //Check if the reservation is not shorter than 30 minutes
            if (reservation.EndTime - reservation.StartTime < TimeSpan.FromMinutes(30))
            {
                return new BadRequestObjectResult("The reservation is shorter than 30 minutes");
            }

            //Get Club
            Club club = court.ClubNavigation;

            //Check if Reservation does not interfere other reservations
            IEnumerable<Reservation> reservations = await _reservationRepository.GetByCourtAndDateRange(court, reservation.StartTime, reservation.EndTime);

            if (reservations.Any())
            {
                return new BadRequestObjectResult("The reservation interferes with another reservation");
            }

            //Create new reservation
            Reservation newReservation = new Reservation(reservation.StartTime, reservation.EndTime, reservation.Comment, court, user, club);

            //Add reservation
            _reservationRepository.Add(newReservation);

            //Create location
            string url = _configuration.GetSection("MvcFrontEnd").Value;
            Uri uri = new(url + "/c/" + club.Link + "/court/" + court.Id + "/reservation/" + newReservation.UUID);

            //Return created
            return new CreatedResult(uri.AbsoluteUri, newReservation);
        }

        public async Task<IActionResult> Delete(string reservationUUID, string uuid)
        {
            //Get User
            User? user = await _userRepository.GetByUUID(uuid);

            //Check if the user exists
            if (user == null)
            {
                return new NotFoundObjectResult("User not found");
            }

            //Get the reservation by uuid
            Reservation? reservation = await _reservationRepository.GetByUUID(reservationUUID);

            //Check if the reservation exists
            if (reservation == null)
            {
                return new NotFoundObjectResult("Reservation not found");
            }

            //Get club
            Club? club = reservation.ClubNavigation;

            if (club == null)
            {
                return new NotFoundObjectResult("Club not found");
            }

            //Check if the user is allowed to delete the reservation
            //The user is allowed when A: The user is the owner of the club
            if (club.Admin != user)
            {
                return new UnauthorizedObjectResult("You are not allowed to delete this reservation");
            }

            //Delete reservation
            _reservationRepository.Delete(reservation);

            //Return no content
            return new NoContentResult();
        }
    }
}

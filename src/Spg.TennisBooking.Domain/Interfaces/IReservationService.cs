using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.ReservationDtos;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IReservationService
    {
        /*
           GetReservationById - Admin
           GetReservationsByClub - Admin
           GetReservationsByCourtId - Only When (Dto as Reponse)
           GetReservationsByUser - Only Themself
           PostReservation
           DeleteReservation - Admin
         */
        Task<IActionResult> GetByUUID(string reservationUUID, string uuid);
        Task<IActionResult> GetByClub(string clubLink, string uuid);
        Task<IActionResult> GetByCourt(int courtId);
        Task<IActionResult> GetByUser(string uuid);
        Task<IActionResult> Post(PostReservationDto reservation, string uuid);
        Task<IActionResult> Delete(string reservationUUID, string uuid);
    }
}
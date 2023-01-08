using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.ReservationDtos;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IReservationService
    {
        /*
         * GetReservationsByCourtId - Only When (Dto as Reponse)
           GetReservationById - Admin
           PostReservation
           DeleteReservation - Admin
           GetReservationsByUser - Only Themself
           GetReservationsByClub - Admin
         */
        Task<IActionResult> GetById(int id, string uuid);
        Task<IActionResult> GetByClub(string clubLink, string uuid);
        Task<IActionResult> GetByCourt(int courtId);
        Task<IActionResult> GetByUser(string uuid);
        Task<IActionResult> Post(PostReservationDto reservation, string uuid);
        Task<IActionResult> Delete(int id, string uuid);
    }
}
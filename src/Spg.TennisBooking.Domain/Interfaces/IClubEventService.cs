using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.ClubEventDtos;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IClubEventService
    {
        /* ClubEvent:
         * Get(id)
         * GetAll(Club Link)
         * Post
         * Put(id)
         * Delete(id)
         */

        Task<IActionResult> Get(int id);
        Task<IActionResult> GetAll(string clubLink);
        Task<IActionResult> Post(PostClubEventDto postClubEventDto, string uuid);
        Task<IActionResult> Put(PutClubEventDto putClubEventDto, string uuid);
        Task<IActionResult> Delete(int id, string uuid);
    }
}
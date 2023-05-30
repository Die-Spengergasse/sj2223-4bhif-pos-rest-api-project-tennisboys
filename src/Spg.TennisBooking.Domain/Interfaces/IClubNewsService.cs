using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.ClubNewsDtos;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IClubNewsService
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
        Task<IActionResult> Post(PostClubNewsDto postClubEventDto, string uuid);
        Task<IActionResult> Put(PutClubNewsDto putClubEventDto, string uuid);
        Task<IActionResult> Delete(int id, string uuid);
    }
}
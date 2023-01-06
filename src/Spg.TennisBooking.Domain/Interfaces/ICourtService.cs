using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.CourtDtos;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface ICourtService
    {
        Task<IActionResult> Get(int id);
        Task<IActionResult> GetAll(string clubLink);
        Task<IActionResult> Post(CourtDto court, string uuid);
        Task<IActionResult> Patch(CourtDto court, string uuid);
        Task<IActionResult> Delete(int id, string uuid);
    }
}
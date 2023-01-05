using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.ClubDtos;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IClubService
    {
        Task<IActionResult> Get(string link);
        Task<IActionResult> Post(PatchClubDto postClubDto);
    }
}
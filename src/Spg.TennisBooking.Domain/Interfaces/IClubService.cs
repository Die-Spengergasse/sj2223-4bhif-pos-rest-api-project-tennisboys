using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.ClubDtos;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IClubService
    {
        Task<IActionResult> Get(string link, string uuid);
        Task<IActionResult> GetAll(string? search);
        Task<IActionResult> Create(string name, string uuid);
        Task<IActionResult> Put(PutClubDto patchClubDto, string uuid);
        Task<IActionResult> Delete(string link, string uuid);
        Task<IActionResult> GetPayementKey(string link, string uuid);
        Task<IActionResult> IsPaid(string link, string uuid);
    }
}
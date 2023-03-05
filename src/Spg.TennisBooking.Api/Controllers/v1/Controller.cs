using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Spg.TennisBooking.Domain.Dtos.UserDtos;
using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;

namespace Spg.TennisBooking.Api.Controllers.v1;
public static class Controller
{
    public static string GetUserId(ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.Name);
    }
}

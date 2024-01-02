using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Interfaces
{
    public interface IBenchmarkService
    {
        Task<IActionResult> GetSQL();
        Task<IActionResult> GetMongo();
    }
}

using Microsoft.AspNetCore.Mvc;
using Spg.TennisBooking.Domain.Dtos.ClubEventDtos;
using Spg.TennisBooking.Domain.Interfaces;
using Spg.TennisBooking.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spg.TennisBooking.BenchmarkMongoSQL;

namespace Spg.TennisBooking.Application.Services.v2
{
    public class BenchmarkService: IBenchmarkService
    {
        public async Task<IActionResult> GetSQL()
        {
            BenchmarkSQL benchmarkSQL = new();
            return benchmarkSQL.Benching();
            return new OkObjectResult("");
        }
        public async Task<IActionResult> GetMongo()
        {
            BenchmarkMongo benchmarkMongo = new();
            return benchmarkMongo.Benching();
            return new OkObjectResult("");
        }
    }
}

using Spg.TennisBooking.Domain.Dtos.ClubDtos;
using Spg.TennisBooking.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.BenchmarkMongoSQL.Dtos
{
    public class CourtRequestDto
    {
        public string ClubName { get; set; }
        public string KW { get; set; }
        public List<CourtDto> Courts { get; set; }
    }
}
/*
 * Filter nach KW
 * {
 * clubInfos
 * courts: [
 *  {
 *      days: [
 *      {
 *         name: "Mo",
 *         reservations: [
 *          {
 *              from: "10:00",
 *              to: "11:00",
 *          }
 */
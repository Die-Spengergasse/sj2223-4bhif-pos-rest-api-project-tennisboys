using Spg.TennisBooking.BenchmarkMongoSQL.Dtos;
using Spg.TennisBooking.Infrastructure.v2;

namespace Spg.TennisBooking.BenchmarkMongoSQL
{
    public interface Benchmark
    {
        //Benchmark Mongo
        //Benchmark SQL
        public void Benching();

        //Connection Aufbau Mongo
        //Connection Aufbau SQL
        public TennisBookingContext GetContext();

        //Mocking Mongo
        //Mocking SQL
        public TennisBookingContext Mocking(TennisBookingContext db);

        //Abfrage Mongo
        //Abfrage SQL
        public CourtRequestDto CourtRequest(TennisBookingContext db);
    }
}
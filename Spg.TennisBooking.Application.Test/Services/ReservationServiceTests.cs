using Spg.TennisBooking.Domain.Exceptions;
using Spg.TennisBooking.Domain.Interfaces;

namespace Spg.TennisBooking.Application.Services
{
    public class ReservationServiceTests
    {
        private readonly IReservationService _reservationService;

        public ReservationServiceTests(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
    }
}

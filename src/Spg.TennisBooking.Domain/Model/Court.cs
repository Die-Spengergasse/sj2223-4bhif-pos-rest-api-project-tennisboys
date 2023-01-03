using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Model
{
    public enum CourtType { Sand = 0, Carpet = 1, Grass = 2, Hard = 3 }

    public class Court
    {
        public int Id { get; private set; }
        public string UUID { get; private set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public CourtType Type { get; set; } = CourtType.Sand;
        public double APrice { get; set; }
        public double? BPrice { get; set; }
        private int _ATimeFrom { get; set; }
        public int ATimeFrom
        {
            get { return _ATimeFrom; }
            set
            {
                if (value < 0 || value > 23)
                {
                    throw new ArgumentException("Time must be between 0 and 23");
                }
                _ATimeFrom = value;
            }
        }
        private int _ATimeTill { get; set; }
        public int ATimeTill
        {
            get
            {
                return _ATimeTill;
            }
            set
            {
                if (value < 0 || value > 24)
                {
                    throw new ArgumentException("Time must be between 0 and 24");
                }
                else
                {
                    _ATimeTill = value;
                }
            }
        }
        private int _AWeekendTimeTill { get; set; }
        public int AWeekendTimeTill
        {
            get
            {
                return _AWeekendTimeTill;
            }
            set
            {
                if (value < 0 || value > 24)
                {
                    throw new ArgumentException("Time must be between 0 and 24");
                }
                else
                {
                    _AWeekendTimeTill = value;
                }
            }
        }

        public virtual int ClubNavigationId { get; set; }
        public virtual Club ClubNavigation { get; set; } = default!;
        public Court(Club club, CourtType type, string name, double aPrice, double? bPrice, int aTimeFrom, int aTimeTill, int aWeekendTimeTill)
        {
            ClubNavigation = club;
            Name = name;
            Type = type;
            APrice = aPrice;
            BPrice = bPrice;
            ATimeFrom = aTimeFrom;
            ATimeTill = aTimeTill;
            AWeekendTimeTill = aWeekendTimeTill;
        }
        
        private readonly List<Reservation> _reservations = new();
        public IReadOnlyList<Reservation> Reservations => _reservations;
        public void AddReservation(Reservation entity)
        {
            if (entity is not null)
            {
                _reservations.Add(entity);
            }
        }
        public void RemoveReservation(Reservation entity)
        {
            if (entity is not null)
            {
                if (_reservations.Contains(entity))
                {
                    _reservations.Remove(entity);
                }
                else
                {
                    throw new ArgumentException("Entity not found");
                }
            }
        
        }

        protected Court() {

        }


    }
}

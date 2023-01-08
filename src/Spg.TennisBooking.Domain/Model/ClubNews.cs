using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Model
{
    public class ClubNews
    {
        public int Id { get; private set; }
        public string Title { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public DateTime Written { get; set; } = DateTime.UtcNow;
        public virtual int ClubNavigationId { get; set; }
        public virtual Club ClubNavigation { get; set; } = default!;

        //Constructer
        public ClubNews(string title, string info, Club clubNavigation)
        {
            Title = title;
            Info = info;
            ClubNavigation = clubNavigation;
        }
        protected ClubNews() {

        }
    }
}

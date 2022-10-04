using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Model
{
    internal class SocialHub
    {
        public int Id { get; set; }
        public string Facebook { get; set; } = string.Empty;
        public string Instagram { get; set; } = string.Empty;
        public string Twitter { get; set; } = string.Empty;
        public string Youtube { get; set; } = string.Empty;
        public string LinkedIn { get; set; } = string.Empty;
        public int Telephone { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.TennisBooking.Domain.Model
{
    public class SocialHub
    {
        public int Id { get; set; }
        private string _facebook { get; set; } = string.Empty;
        public string Facebook
        {
            get => _facebook;
        }

        public string Instagram { get; set; } = string.Empty;
        public string Twitter { get; set; } = string.Empty;
        public string Youtube { get; set; } = string.Empty;
        public string LinkedIn { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;

        public SocialHub()
        {
            
        }

        //TODO: Set Methods for all properties

        public void ChangeFacebook(string content)
        {
            _facebook = content;
        }
    }
}

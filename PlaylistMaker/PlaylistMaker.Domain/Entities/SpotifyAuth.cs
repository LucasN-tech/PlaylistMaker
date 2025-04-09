using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistMaker.Domain.Entities
{
    public class SpotifyAuth
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }    
        public string AccessToken { get; set; }
        public string RedirectUri { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string TokenType { get; set; }
    }
}

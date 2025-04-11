using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistMaker.Domain.Interfaces
{
    public interface ISpotifyAuthService
    {
        Task<string> GetAccessTokenAsync();
    }
}

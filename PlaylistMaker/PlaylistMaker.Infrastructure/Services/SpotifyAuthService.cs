using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using PlaylistMaker.Domain.Entities;
using PlaylistMaker.Domain.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;


namespace PlaylistMaker.Infrastructure.Services
{
    public class SpotifyAuthService : ISpotifyAuthService
    {
        private readonly SpotifyAuth _spotifyAuth;

        public SpotifyAuthService(IOptions<SpotifyAuth> options)
        {
            _spotifyAuth = options.Value;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var client = new HttpClient();
            var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_spotifyAuth.ClientId}:{_spotifyAuth.ClientSecret}"));

            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "client_credentials"}
            });

            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
    }
}

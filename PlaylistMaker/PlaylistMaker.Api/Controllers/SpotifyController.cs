using Microsoft.AspNetCore.Mvc;
using PlaylistMaker.Domain.Interfaces;

namespace PlaylistMaker.Api.Controllers
{
    public class SpotifyController : ControllerBase
    {
        private readonly ILogger<SpotifyController> _logger;
        private readonly ISpotifyAuthService _authService;
        public SpotifyController(ILogger<SpotifyController> logger, ISpotifyAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpGet("api/spotify")]
        public IActionResult Get()
        {
            return Ok("Spotify API is working");
        }

        [HttpGet("token")]
        public async Task<IActionResult> GetToken()
        {
            var token = await _authService.GetAccessTokenAsync();
            return Ok(token);
        }
    }
}
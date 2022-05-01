using Microsoft.AspNetCore.Mvc;
using OnlineAuction.API.Services;

namespace OnlineAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        //[HttpGet("getRefreshToken")]
        //public IActionResult GetRefreshToken() 
        //{
        //    return Ok(_authService.GetRefreshToken());
        //}
    }
}

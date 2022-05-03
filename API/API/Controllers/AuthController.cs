using Microsoft.AspNetCore.Mvc;
using OnlineAuction.API.Models;
using OnlineAuction.API.Services;
using System.Threading.Tasks;

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

        [HttpPost("logIn")]
        public async Task<IActionResult> LogIn([FromForm] LoginRequest loginRequest)
        {
            return Ok(await _authService.LogIn(loginRequest));
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromForm] SignUpRequest signUpRequest)
        {
            return Ok(await _authService.SignUp(signUpRequest));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using OnlineAuction.API.Models.Requests;
using OnlineAuction.API.Services;
using System;
using System.Threading.Tasks;

namespace OnlineAuction.API.Controllers
{
    public class AuthController: BaseController
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService, IServiceProvider sp) : base(sp)
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

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenRequest refreshTokenRequest) 
        {
            return Ok(await _authService.Refresh(refreshTokenRequest));
        }

        [HttpPost("pass")]
        public async Task<IActionResult> GenPass([FromForm] string password) 
        {
            return Ok(await _authService.GenPass(password));
        }
    }
}

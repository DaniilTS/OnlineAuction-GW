using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OnlineAuction.API.Services;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace OnlineAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("{id}/block")]
        [Authorize]
        public async Task<IActionResult> BlockUser(Guid id, [FromBody] bool state)
        {
            await _userService.SetBlockedState(id, state);
            return Ok();
        }

        [HttpPost("{id}/delete")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(Guid id, [FromBody] bool state)
        {
            await _userService.SetDeletedState(id, state);
            return Ok();
        }

        [HttpPost("photo/upload")]
        [Authorize]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _userService.GetUserByEmail(email);
            await _userService.UploadUserPhoto(file, user.Id);
            return Ok();
        }
    }
}

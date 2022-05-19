using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OnlineAuction.API.Services;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using OnlineAuction.API.Models.Requests;
using OnlineAuction.DBAL;

namespace OnlineAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserService _userService;
        public UserController(UserService userService, IServiceProvider sp) : base(sp)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetUsers([FromQuery] PaginationParams request) 
        {
            return Ok(await _userService.GetUsers(request));
        }

        [HttpPost("{id}/block")]
        [Authorize]
        public async Task<IActionResult> BlockUser(Guid id, [FromForm] bool state)
        {
            await _userService.SetBlockedState(id, state);
            return Ok();
        }

        [HttpPost("{id}/delete")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(Guid id, [FromForm] bool state)
        {
            await _userService.SetDeletedState(id, state);
            return Ok();
        }

        [HttpPost("photo")]
        [Authorize]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            await _userService.UploadUserPhoto(file, CurrentUser.Id);
            return Ok();
        }

        [HttpGet("photo")]
        [Authorize]
        public async Task<IActionResult> GetPhoto() 
        {
            return Ok((await _userService.GetUserPhoto(CurrentUser.Id)).Url);
        }


        [HttpGet("")]
        [Authorize]
        public IActionResult GetUser() 
        {
            return Ok(CurrentUser);
        }
    }
}

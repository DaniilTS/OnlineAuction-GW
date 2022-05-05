using OnlineAuction.API.Helpers;
using OnlineAuction.DBAL.Operations;
using OnlineAuction.DBAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OnlineAuction.API.Services;
using System;

namespace OnlineAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("{userGuid:Guid}/block")]
        public async Task<IActionResult> BlockUser(Guid userGuid, [FromBody] bool state)
        {
            await _userService.SetBlockedState(userGuid, state);
            return Ok();
        }

        [HttpPost("{userGuid:Guid}/delete")]
        public async Task<IActionResult> DeleteUser(Guid userGuid, [FromBody] bool state)
        {
            await _userService.SetDeletedState(userGuid, state);
            return Ok();
        }
    }
}

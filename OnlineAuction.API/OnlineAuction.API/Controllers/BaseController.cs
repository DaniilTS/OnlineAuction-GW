using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OnlineAuction.API.Services;
using OnlineAuction.DBAL.Models;
using System;

namespace OnlineAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController: ControllerBase
    {
        public User CurrentUser { get; set; }
        public BaseController(IServiceProvider sp) 
        {
            var httpContextAccessor = sp.GetService<IHttpContextAccessor>();

            var userService = sp.GetService<UserService>();

            var userEmail = httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (userEmail is not null) 
            {
                CurrentUser = userService.GetUserByEmail(userEmail).Result;
            }
        }
    }
}

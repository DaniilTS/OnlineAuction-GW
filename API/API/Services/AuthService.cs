using Microsoft.Extensions.DependencyInjection;
using OnlineAuction.JWT.Auth.Models;
using OnlineAuction.JWT.Auth.Services;
using System;

namespace OnlineAuction.API.Services
{
    public class AuthService
    {
        private readonly TokenService _tokenService;
        public AuthService(IServiceProvider serviceProvider) 
        {
            _tokenService = serviceProvider.GetService<TokenService>();
        }



    }
}

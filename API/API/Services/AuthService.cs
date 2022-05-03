using Microsoft.Extensions.DependencyInjection;
using OnlineAuction.API.Constants;
using OnlineAuction.API.Exceptions;
using OnlineAuction.API.Helpers;
using OnlineAuction.API.Models;
using OnlineAuction.DBAL.Models;
using OnlineAuction.DBAL.Operations;
using OnlineAuction.DBAL.Repositories;
using OnlineAuction.JWT.Auth.Services;
using System;
using System.Threading.Tasks;

namespace OnlineAuction.API.Services
{
    public class AuthService
    {
        private readonly TokenService _tokenService;

        private readonly UserRepository _userRepository;
        private readonly FullNameRepository _fullNameRepository;

        private readonly RoleOperation _roleOperation;
        public AuthService(IServiceProvider serviceProvider) 
        {
            _tokenService = serviceProvider.GetService<TokenService>();

            _userRepository = serviceProvider.GetService<UserRepository>();
            _fullNameRepository = serviceProvider.GetService<FullNameRepository>();

            _roleOperation = serviceProvider.GetService<RoleOperation>();
        }

        public async Task<LoginResponse> LogIn(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetObject(loginRequest.Email);
            if(user is null)
                throw new UnauthorizedAccessException(ExceptionConstants.PasswordOrEmailIsNotRight);

            var password = await CryptoHelper.GetHash(string.Join(loginRequest.Password, user.Salt));
            if (user.Password != password)
                throw new UnauthorizedAccessException(ExceptionConstants.PasswordOrEmailIsNotRight);

            return new LoginResponse
            {
                AccessToken = _tokenService.GenerateAccessToken(user.Email, user.Role.Name),
                RefreshToken = _tokenService.GenerateRefreshToken()
            };
        }

        public async Task<SignUpResponse> SignUp(SignUpRequest signUpRequest) 
        {
            var email = signUpRequest.Email;
            var phone = signUpRequest.Phone;

            if (await _userRepository.IsObjectExists(email, phone)) 
            {
                throw new UserIsAlreadyExistsException(ExceptionConstants.UserIsAlreadyExists);
            }

            var salt = CryptoHelper.GenerateSalt();
            var password = await CryptoHelper.GetHash(string.Join(signUpRequest.Password, salt));

            var clientRole = _roleOperation.Client;

            var accessToken = _tokenService.GenerateAccessToken(email, clientRole.Name);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var fullName = new FullName()
            {
                FirstName = signUpRequest.FirstName,
                SecondName = signUpRequest.SecondName,
                ThirdName = signUpRequest.ThirdName
            };

            var fullNameId = (await _fullNameRepository.GetOrCreate(fullName)).Id;

            await _userRepository.CreateObject(new User 
            { 
                Id = Guid.NewGuid(),
                RoleId = clientRole.Id,
                GenderId = signUpRequest.GenderId,
                FullNameId = fullNameId,
                Email = email,
                Phone = phone,
                Password = password,
                Salt = salt,
                RefreshToken = refreshToken.Value,
                IsBlocked = false,
                IsDeleted = false,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            });

            return new SignUpResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}

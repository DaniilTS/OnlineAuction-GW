using Microsoft.Extensions.DependencyInjection;
using OnlineAuction.API.Helpers;
using OnlineAuction.API.Models.Requests;
using OnlineAuction.API.Models.Responses;
using OnlineAuction.API.Models.Shared;
using OnlineAuction.Common.Domain;
using OnlineAuction.Common.Domain.Exceptions;
using OnlineAuction.DBAL.Context;
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
        private readonly OnlineAuctionContext _context;
        private readonly TokenService _tokenService;

        private readonly UserRepository _userRepository;
        private readonly FullNameRepository _fullNameRepository;
        private readonly PocketRepository _pocketRepository;

        private readonly RoleOperation _roleOperation;
        public AuthService(IServiceProvider serviceProvider) 
        {
            _context = serviceProvider.GetService<OnlineAuctionContext>();

            _tokenService = serviceProvider.GetService<TokenService>();

            _userRepository = serviceProvider.GetService<UserRepository>();
            _fullNameRepository = serviceProvider.GetService<FullNameRepository>();
            _pocketRepository = serviceProvider.GetService<PocketRepository>();

            _roleOperation = serviceProvider.GetService<RoleOperation>();
        }

        public async Task<AuthBase> LogIn(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetObject(loginRequest.Email);
            if(user is null)
                throw new UnauthorizedAccessException(ExceptionConstants.PasswordOrEmailIsNotRight);

            var password = await CryptoHelper.GetHash(string.Join(loginRequest.Password, user.Salt));
            if (user.Password != password)
                throw new UnauthorizedAccessException(ExceptionConstants.PasswordOrEmailIsNotRight);

            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken.Value;
            await _userRepository.UpdateObject(user);

            return new LoginResponse
            {
                AccessToken = _tokenService.GenerateAccessToken(user.Email, user.Role.Name),
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthBase> SignUp(SignUpRequest signUpRequest) 
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

            var user = new User
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
            };

            await ProcessUserCreation(user);

            return new SignUpResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthBase> Refresh(RefreshTokenRequest refreshTokenRequest)
        {
            var accessToken = refreshTokenRequest.AccessToken;
            var refreshToken = refreshTokenRequest.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            if (principal is null)
                throw new UnauthorizedAccessException();

            var email = principal.Identity?.Name;
            var user = await _userRepository.GetObject(email);

            if (user is null || user.RefreshToken != refreshToken)
                throw new UnauthorizedAccessException(ExceptionConstants.RefreshTokenIsNotValid);

            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken.Value;
            await _userRepository.UpdateObject(user);

            return new AuthBase
            {
                AccessToken = _tokenService.GenerateAccessToken(user.Email, user.Role.Name),
                RefreshToken = newRefreshToken
            };
        }

        private async Task ProcessUserCreation(User user) 
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _userRepository.CreateObject(user);

                var pocket = new Pocket()
                {
                    Id = Guid.NewGuid(),
                    HolderId = user.Id,
                    Amount = 0
                };

                await _pocketRepository.CreateObject(pocket);

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }          
        } 
    }
}

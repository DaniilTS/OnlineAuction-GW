using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OnlineAuction.API.Models.Helpers;
using OnlineAuction.Common.Domain;
using OnlineAuction.DBAL.Context;
using OnlineAuction.DBAL.Models;
using OnlineAuction.DBAL.Repositories;
using System;
using System.Threading.Tasks;

namespace OnlineAuction.API.Services
{
    public class UserService
    {
        private readonly OnlineAuctionContext _context;
        private readonly UserRepository _userRepository;
        private readonly PhotoService _photoService;
        private readonly UserImageRepository _userImageRepository;

        public UserService(IServiceProvider provider)
        {
            _userRepository = provider.GetService<UserRepository>();
            _userImageRepository = provider.GetService<UserImageRepository>();
            _photoService = provider.GetService<PhotoService>();
            _context = provider.GetService<OnlineAuctionContext>();
        }

        public async Task<User> GetUserByEmail(string email) => await _userRepository.GetObject(email);

        public async Task SetBlockedState(Guid userId, bool state)
        {
            var user = await _userRepository.GetObject(userId);
            user.IsBlocked = state;
            await _userRepository.UpdateObject(user);
        }

        public async Task SetDeletedState(Guid userId, bool state)
        {
            var user = await _userRepository.GetObject(userId);
            user.IsDeleted = state;
            await _userRepository.UpdateObject(user);
        }

        public async Task UploadUserPhoto(IFormFile file, Guid userId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync()) 
            {
                try
                {
                    var userImage = await _userImageRepository.GetObject(userId);
                    if (userImage is not null)
                        await DeleteUserPhoto(userImage);

                    var uploadResult = await _photoService.UploadPhoto(file, userId, "Users");

                    var image = new UserImage
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        Url = uploadResult.SecureUrl.AbsoluteUri,
                        IsDeleted = false,
                        Created = DateTime.UtcNow,
                        Deleted = null
                    };

                    await _userImageRepository.CreateObject(image);

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(ExceptionConstants.PhotoUploadFailed);
                }
            }      
        }

        private async Task DeleteUserPhoto(UserImage userImage)
        {
            userImage.IsDeleted = true;
            await _userImageRepository.UpdateObject(userImage);
        }

        
    }
}

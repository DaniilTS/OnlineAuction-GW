using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OnlineAuction.API.Models.Helpers;
using OnlineAuction.DBAL.Models;
using OnlineAuction.DBAL.Repositories;
using System;
using System.Threading.Tasks;

namespace OnlineAuction.API.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly UserImageRepository _userImageRepository;
        private readonly Cloudinary _cloudinary;

        public UserService(IServiceProvider provider, IOptions<CloudinarySettings> config)
        {
            _userRepository = provider.GetService<UserRepository>();
            _userImageRepository = provider.GetService<UserImageRepository>();

            _cloudinary = new Cloudinary(new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret));
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
            var userImage = await _userImageRepository.GetObject(userId);
            if (userImage is not null)
                await DeleteUserPhoto(userImage);

            var uploadResult = await UploadPhoto(file, userId);

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
        }

        private async Task DeleteUserPhoto(UserImage userImage)
        {
            userImage.IsDeleted = true;
            await _userImageRepository.UpdateObject(userImage);
        }

        private async Task<ImageUploadResult> UploadPhoto(IFormFile file, Guid userId)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var fileName = $"{Guid.NewGuid()}";
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName, stream),
                    PublicId = $"OnlineAuction/Users/{userId}/{fileName}",
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }
    }
}

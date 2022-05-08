using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OnlineAuction.API.Models.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.API.Services
{
    public class PhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> config) 
        {
            var configValue = config.Value;
            _cloudinary = new Cloudinary(new Account(
                configValue.CloudName,
                configValue.ApiKey,
                configValue.ApiSecret));
        }

        public async Task<List<ImageUploadResult>> UploadPhotos(IFormFileCollection formFiles, Guid id, string folderName) 
        {
            var list = new List<ImageUploadResult>();

            foreach (var file in formFiles) 
            {
                list.Add(await UploadPhoto(file, id, folderName));
            }

            return list;
        }

        public async Task<ImageUploadResult> UploadPhoto(IFormFile file, Guid id, string folderName)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var fileName = $"{Guid.NewGuid()}";

                var isUser = folderName == "Users";
                var transformation = isUser
                    ? new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                    : new Transformation().Crop("fill");

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName, stream),
                    PublicId = $"OnlineAuction/{folderName}/{id}/{fileName}",
                    Transformation = transformation
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }
    }
}

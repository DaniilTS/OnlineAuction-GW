using Microsoft.Extensions.DependencyInjection;
using OnlineAuction.DBAL.Repositories;
using OnlineAuction.DBAL.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.API.Models.Requests;
using CloudinaryDotNet.Actions;
using System.Linq;
using OnlineAuction.DBAL.Context;
using OnlineAuction.Common.Domain;

namespace OnlineAuction.API.Services
{
    public class LotService
    {
        private readonly LotCategoryRepository lotCategoryRepository;
        private readonly LotRepository lotRepository;
        private readonly LotImageRepository lotImageRepository;
        private readonly PhotoService photoService;
        private readonly OnlineAuctionContext context;
        public LotService(IServiceProvider serviceProvider)
        {
            lotCategoryRepository = serviceProvider.GetService<LotCategoryRepository>();
            lotRepository = serviceProvider.GetService<LotRepository>();
            lotImageRepository = serviceProvider.GetService<LotImageRepository>();
            photoService = serviceProvider.GetService<PhotoService>();
            context = serviceProvider.GetService<OnlineAuctionContext>();
        }

        #region [Lot Category]
        public async Task CreateLotCategory([FromForm] string name)
        {
            await lotCategoryRepository.CreateObject(new LotCategory
            {
                Id = Guid.NewGuid(),
                Name = name
            });
        }

        public async Task<IEnumerable<LotCategory>> GetLotCategories()
        {
            return await lotCategoryRepository.GetCollection();
        }

        public async Task DeleteLotCategory(Guid id) => await lotCategoryRepository.DeleteObject(id);

        #endregion

        #region [Lot]

        public async Task CreateLot(Guid userId, LotCreateRequest request) 
        {
            using (var transaction = await context.Database.BeginTransactionAsync()) 
            {
                try
                {
                    var lot = new Lot
                    {
                        Id = Guid.NewGuid(),
                        LotCategoryId = request.LotCategoryId,
                        Description = request.Description,
                        CreatorId = userId,
                        IsSubmitted = false
                    };

                    await lotRepository.CreateObject(lot);

                    var imageUploadResults = await photoService.UploadPhotos(request.FormFiles, lot.Id, "Lots");
                    await CreateLotImages(imageUploadResults, lot.Id);

                    await transaction.CommitAsync();
                }
                catch (Exception) 
                {
                    await transaction.RollbackAsync();
                    throw new Exception(ExceptionConstants.LotCreationProccessFailed);
                }              
            }
        }

        private async Task CreateLotImages(List<ImageUploadResult> imageUploadResults, Guid lotId) 
        {
            var images = imageUploadResults.Select(x => new LotImage
            {
                Id = Guid.NewGuid(),
                LotId = lotId,
                Url = x.SecureUrl.AbsoluteUri,
                IsDeleted = false,
                Created = DateTime.UtcNow,
                Deleted = DateTime.UtcNow
            });

            await lotImageRepository.CreateObjects(images);
        }

        public async Task LotSubmition(Guid id, [FromForm] bool submitValue) 
        {
            var lot = await lotRepository.GetObject(id);
            lot.IsSubmitted = submitValue;
            await lotRepository.UpdateObject(lot);
        }

        #endregion
    }
}
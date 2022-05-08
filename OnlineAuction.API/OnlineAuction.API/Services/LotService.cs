using Microsoft.Extensions.DependencyInjection;
using OnlineAuction.DBAL.Repositories;
using OnlineAuction.DBAL.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.API.Models.Requests;

namespace OnlineAuction.API.Services
{
    public class LotService
    {
        private readonly LotCategoryRepository lotCategoryRepository;
        private readonly LotRepository lotRepository;
        public LotService(IServiceProvider serviceProvider)
        {
            lotCategoryRepository = serviceProvider.GetService<LotCategoryRepository>();
            lotRepository = serviceProvider.GetService<LotRepository>();
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

        public async Task CreateLot(LotCreateRequest request) 
        { 
            
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
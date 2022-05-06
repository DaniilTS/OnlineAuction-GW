using Microsoft.Extensions.DependencyInjection;
using OnlineAuction.DBAL.Repositories;
using OnlineAuction.DBAL.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OnlineAuction.API.Services
{
    public class LotService
    {
        private readonly LotCategoryRepository _lotCategoryRepository;
        public LotService(IServiceProvider serviceProvider)
        {
            _lotCategoryRepository = serviceProvider.GetService<LotCategoryRepository>();
        }

        public async Task CreateLotCategory(string name) 
        {
            await _lotCategoryRepository.CreateObject(new LotCategory
            {
                Id = Guid.NewGuid(),
                Name = name
            });
        }

        public async Task<IEnumerable<LotCategory>> GetLotCategories()
        {
            return await _lotCategoryRepository.GetCollection();
        }

        public void DeleteLotCategory(Guid id) => _lotCategoryRepository.DeleteObject(id);
    }
}

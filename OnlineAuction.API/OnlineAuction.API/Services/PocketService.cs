using OnlineAuction.DBAL.Repositories;
using System;
using System.Threading.Tasks;

namespace OnlineAuction.API.Services
{
    public class PocketService
    {
        private readonly PocketRepository _pocketRepository;
        public PocketService(PocketRepository pocketRepository)
        {
            _pocketRepository = pocketRepository;
        }

        public async Task<decimal> GetPocketAmount(Guid userGuid)
        {
            return (await _pocketRepository.GetObject(userGuid)).Amount;
        }

        public async Task SetPocketAmount(Guid pocketGuid, decimal amount)
        {
            var pocket = await _pocketRepository.GetObject(pocketGuid);
            pocket.Amount = amount;
            await _pocketRepository.UpdateObject(pocket);
        }
    }
}

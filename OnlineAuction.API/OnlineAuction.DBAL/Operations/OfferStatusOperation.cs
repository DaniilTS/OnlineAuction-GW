using OnlineAuction.Common.Domain.Constants;
using OnlineAuction.DBAL.Models;
using OnlineAuction.DBAL.Repositories;

namespace OnlineAuction.DBAL.Operations
{
    public class OfferStatusOperation
    {
        private readonly OfferStatusRepository _repo;
        public OfferStatusOperation(OfferStatusRepository repo)
        {
            _repo = repo;
        }

        public OfferStatus Accepted => _repo.GetObject(OfferStatuses.Accepted).Result;
        public OfferStatus Declined => _repo.GetObject(OfferStatuses.Declined).Result;
        public OfferStatus Pending => _repo.GetObject(OfferStatuses.Pending).Result;
    }
}

using OnlineAuction.DBAL.Constants;
using OnlineAuction.DBAL.Models;
using OnlineAuction.DBAL.Repositories;

namespace OnlineAuction.DBAL.Operations
{
    public class GenderOperation
    {
        private readonly GenderRepository _repo;
        public GenderOperation(GenderRepository repo)
        {
            _repo = repo;
        }

        public Gender Male => _repo.GetObject(Genders.Male).Result;
        public Gender Female => _repo.GetObject(Genders.Female).Result;
    }
}

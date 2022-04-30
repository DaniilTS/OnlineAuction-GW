using DBAL.Constants;
using DBAL.Models;
using DBAL.Repositories;

namespace DBAL.Operations
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

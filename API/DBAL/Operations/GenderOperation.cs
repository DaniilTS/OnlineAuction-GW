using DBAL.Constants;
using DBAL.Models;
using DBAL.Repositories;

namespace DBAL.Operations
{
    public class GenderOperation
    {
        private readonly GenderRepository _genderRepository;
        public GenderOperation(GenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public Gender Male => _genderRepository.GetObject(Genders.Male).Result;
        public Gender Female => _genderRepository.GetObject(Genders.Female).Result;
    }
}

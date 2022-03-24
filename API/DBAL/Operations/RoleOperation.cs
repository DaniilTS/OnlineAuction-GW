using DBAL.Constants;
using DBAL.Models;
using DBAL.Repositories;

namespace DBAL.Operations
{
    public class RoleOperation
    {
        private readonly RoleRepository _roleRepository;
        public RoleOperation(RoleRepository roleRepository) 
        {
            _roleRepository = roleRepository;
        }

        public Role Admin => _roleRepository.GetObject(Roles.Admin).Result;
        public Role Employee => _roleRepository.GetObject(Roles.Employee).Result;
        public Role Client => _roleRepository.GetObject(Roles.Client).Result;
    }
}

using OnlineAuction.DBAL.Constants;
using DBAL.Models;
using OnlineAuction.DBAL.Repositories;

namespace OnlineAuction.DBAL.Operations
{
    public class RoleOperation
    {
        private readonly RoleRepository _repo;
        public RoleOperation(RoleRepository repo) 
        {
            _repo = repo;
        }

        public Role Admin => _repo.GetObject(Roles.Admin).Result;
        public Role Employee => _repo.GetObject(Roles.Employee).Result;
        public Role Client => _repo.GetObject(Roles.Client).Result;
    }
}

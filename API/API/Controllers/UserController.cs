using API.Helpers;
using DBAL.Operations;
using DBAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RoleRepository _roleRepository;
        private readonly RoleOperation _roleOperation;
        public UserController(RoleRepository roleRepository, RoleOperation roleOperation) 
        {
            _roleRepository = roleRepository;
            _roleOperation = roleOperation;
        }

        [HttpGet("sha")]
        public async Task<IActionResult> GetSHA512(string input) 
        {
            return Ok(await SHA512.GetHash(input));
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles() 
        {
            return Ok(await _roleRepository.GetCollection());
        }
    }
}

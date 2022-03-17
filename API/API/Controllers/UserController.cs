using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetSHA512(string input) 
        {
            return Ok(await SHA512.GetHash(input));
        }
    }
}

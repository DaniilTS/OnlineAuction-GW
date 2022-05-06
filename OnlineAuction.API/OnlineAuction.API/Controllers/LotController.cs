using Microsoft.AspNetCore.Mvc;
using OnlineAuction.API.Services;
using System;
using System.Threading.Tasks;

namespace OnlineAuction.API.Controllers
{
    public class LotController: BaseController
    {
        private readonly LotService _lotService;
        public LotController(LotService lotService)
        {
            _lotService = lotService;
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetLotCategories() 
        {
            return Ok(await _lotService.GetLotCategories());
        }

        [HttpPost("category")]
        public async Task<IActionResult> CreateLotCategory([FromForm] string lotCategoryName)
        {
            await _lotService.CreateLotCategory(lotCategoryName);
            return Ok();
        }

        [HttpDelete("category")]
        public IActionResult DeleteLotCategory([FromForm] Guid id) 
        {
            _lotService.DeleteLotCategory(id);
            return Ok();
        }

        [HttpPost("")]
        public IActionResult CreateLot() 
        {
            return Ok();
        }

        [HttpPost("{id}/submit")]
        public IActionResult SubmitLot(Guid id) 
        {
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.API.Models.Requests;
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

        #region [Lot Categories]

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
        public async Task<IActionResult> DeleteLotCategory([FromForm] Guid id)
        {
            await _lotService.DeleteLotCategory(id);
            return Ok();
        }

        #endregion

        #region [Lot]

        [HttpPost("")]
        [Authorize]
        public async Task<IActionResult> CreateLot([FromForm] LotCreateRequest request)
        {
            await _lotService.CreateLot(request);
            return Ok();
        }

        [HttpPost("{id}/submit")]
        public async Task<IActionResult> SubmitLotValue(Guid id, [FromForm] bool value)
        {
            await _lotService.LotSubmition(id, value);
            return Ok();
        }

        #endregion
    }
}

﻿using Microsoft.AspNetCore.Mvc;
using OnlineAuction.API.Models.Requests;
using OnlineAuction.API.Services;
using System;
using System.Threading.Tasks;

namespace OnlineAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PocketController : ControllerBase
    {
        private readonly PocketService _pocketService;
        public PocketController(PocketService pocketService)
        {
            _pocketService = pocketService;
        }

        [HttpGet("getPocketAmount")]
        public async Task<IActionResult> GetPocketAmount([FromQuery] Guid pocketGuid)
        {
            return Ok(await _pocketService.GetPocketAmount(pocketGuid));
        }

        [HttpPost("setPocketAmount")]
        public async Task<IActionResult> SetPocketAmount([FromForm] SetPocketAmountRequest request)
        {
            await _pocketService.SetPocketAmount(request.PocketGuid, request.PocketAmount);
            return Ok();
        }
    }
}
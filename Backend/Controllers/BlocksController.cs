using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Models.DTOs;
using Backend.Services;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlocksController : ControllerBase
    {
        private readonly BlockServices _blockService;

        public BlocksController(BlockServices service)
        {
            _blockService = service;
        }

        [HttpPost]
        [Route("userDocuments")]
        public async Task<IActionResult> GetUserBlocks(int owner)
        {
            var documents = await _blockService.GetBlocksbyOwner(owner);
            return Ok(new { value = documents });
        }
    }
}

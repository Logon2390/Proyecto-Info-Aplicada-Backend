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
using System.Text;
using Backend.Custom;

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
        [Route("userBlocks")]
        public async Task<IActionResult> GetUserBlocks(int owner)
        {
            var blocks = await _blockService.GetBlocksbyOwner(owner);
            return Ok(new { value = blocks });
        }

        [HttpGet]
        [Route("findBlock")]
        public async Task<IActionResult> GetBlock(int id)
        {
            var block = await _blockService.GetBlockById(id);
            return Ok(new { value = block });
        }

        [HttpPost]
        [Route("mineBlocks")]
        public async Task<IActionResult> MineBlocks(BlockDto block, int ownerId)
        {
            //Primero se crean los bloques con los documentos
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < block.Documentos.Count; i += 3)
            {
                builder.Append(block.Documentos[i].base64 + "-");
                if (i + 1 < block.Documentos.Count)
                {
                    builder.Append(block.Documentos[i + 1].base64 + "-");
                }
                if (i + 2 < block.Documentos.Count)
                {
                    builder.Append(block.Documentos[i + 2].base64 + "-");
                }
                await _blockService.AddBlockToUser(builder.ToString(), ownerId);
                builder.Clear();
            }
            return Ok();
        }
    }
}

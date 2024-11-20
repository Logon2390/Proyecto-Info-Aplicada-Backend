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
        private readonly DocumentService _documentService;

        public BlocksController(BlockServices service, DocumentService documentService)
        {
            _blockService = service;
            _documentService = documentService;
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
        public async Task<IActionResult> MineBlocks(String owner)
        {
            //Primero se obtienen los documentos del usuario
            var documents = await _documentService.GetUserDocuments(owner);
            //Ahora se crean los bloques con los documentos
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < documents.Count; i += 3)
            {
                builder.Append(documents[i].Id + "-");
                await _documentService.UpdateSize(documents[i].Id);
                if (i + 1 < documents.Count)
                {
                    builder.Append(documents[i + 1].Id + "-");
                    await _documentService.UpdateSize(documents[i+1].Id);
                }
                if (i + 2 < documents.Count)
                {
                    builder.Append(documents[i + 2].Id + "-");
                    await _documentService.UpdateSize(documents[i+2].Id);
                }
                await _blockService.AddBlockToUser(builder.ToString(), owner);
                builder.Clear();
            }
            return Ok();
        }
    }
}

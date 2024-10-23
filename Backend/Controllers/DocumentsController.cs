using Backend.Models;
using Backend.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentContext _context;

        public DocumentsController(DocumentContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getDocuments")]
        public async Task<IActionResult> GetDocuments()
        {
            var documents = await _context.Documents.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new {value = documents});
        }

        [HttpPost]
        [Route("addDocument")]
        public async Task<IActionResult> AddDocument(DocumentDTO documentDTO)
        {
            Document document = new Document
            {
                owner = documentDTO.owner,
                type = documentDTO.type,
                CreatedAt = documentDTO.CreatedAt,
                size = documentDTO.size,
                base64 = documentDTO.base64
            };

            object value = await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();

            if (document.Id != 0)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
            }
        }
    }
}

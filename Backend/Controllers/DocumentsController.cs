using Backend.Models;
using Backend.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Backend.Services;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentService _documentService;

        public DocumentsController(DocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost]
        [Route("userDocuments")]
        public async Task<IActionResult> GetUserDocuments(string owner)
        {
            var documents = await _documentService.GetUserDocuments(owner);
            return Ok(new { value = documents });
        }

        [HttpGet]
        [Route("getDocuments")]
        public async Task<IActionResult> GetDocuments()
        {
            var documents = await _documentService.GetDocuments();
            return Ok(new { value = documents });
        }

        [HttpPost]
        [Route("addDocument")]
        public async Task<IActionResult> AddDocument(DocumentDTO documentDTO)
        {
            var isSuccess = await _documentService.AddDocument(documentDTO);
            return Ok(new { isSuccess });
        }

        [HttpDelete]
        [Route("deleteDocument")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var isSuccess = await _documentService.DeleteDocument(id);
            return Ok(new { isSuccess });
        }
    }
}

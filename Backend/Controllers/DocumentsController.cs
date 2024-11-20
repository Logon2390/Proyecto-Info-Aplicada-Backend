using Backend.Models.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentService _documentService;
        private readonly LogServices _logServices;

        public DocumentsController(DocumentService documentService, LogServices logServices)
        {
            _documentService = documentService;
            _logServices = logServices;

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
        public async Task<IActionResult> AddDocument(DocumentDto documentDTO)
        {
            await _logServices.SaveLog(documentDTO.owner, "Ha agregado un documento");
            var isSuccess = await _documentService.AddDocument(documentDTO);

            return Ok(new { isSuccess });
        }

        [HttpDelete]
        [Route("deleteDocument")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            //obtener el documento
            var document = await _documentService.GetDocument(id);
            if (document == null)
            {
                return NotFound(new { message = "Documento no encontrado" });
            }
            await _logServices.SaveLog(document.owner, "Ha eliminado un documento");
            var isSuccess = await _documentService.DeleteDocument(id);
            return Ok(new { isSuccess });
        }

        [HttpDelete]
        [Route("deleteDocuments")]
        public async Task<IActionResult> DeleteDocuments([FromBody] int[] ids)
        {
            var isSuccess = await _documentService.DeleteDocuments(ids);
            return Ok(new { isSuccess });
        }

        [HttpGet]
        [Route("getBase64")]
        public async Task<IActionResult> GetBase64(int id)
        {
            var document = await _documentService.GetBase64(id);
            return Ok(new { value = document });
        }

        [HttpGet]
        [Route("getDocument")]
        public async Task<IActionResult> GetDocument(int id)
        {
            var document = await _documentService.GetDocument(id);
            return Ok(new { value = document });
        }

        [HttpGet]
        [Route("getLogs")]
        public async Task<IActionResult> GetLogs()
        {
            var logs = await _logServices.GetAllLogs();
            return Ok(new { value = logs });
        }
    }
}

using Backend.Models;
using Backend.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class DocumentService
    {
        private readonly DocumentContext _context;
        private readonly RelationDocumentBase64Context _documentBase64Context;

        public DocumentService(DocumentContext context, RelationDocumentBase64Context documentBase64Context)
        {
            _context = context;
            _documentBase64Context = documentBase64Context;
        }

        public async Task<List<Document>> GetUserDocuments(string owner)
        {
            return await _context.Documents.Where(d => d.owner == owner && d.size > 0).ToListAsync();
        }

        public async Task<List<Document>> GetDocuments()
        {
            return await _context.Documents.ToListAsync();
        }

        public async Task<bool> AddDocument(DocumentDto documentDTO)
        {
            Document document = new Document
            {
                owner = documentDTO.owner,
                type = documentDTO.type,
                CreatedAt = documentDTO.CreatedAt,
                size = documentDTO.size,
                base64 = documentDTO.base64
            };

            String base64 = documentDTO.base64;
            document.base64 = "";

            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();

            RelationDocumentBase64 relationDocumentBase64 = new RelationDocumentBase64
            {
                DocumentId = document.Id,
                Base64 = base64
            };
            await _documentBase64Context.Document_Base64.AddAsync(relationDocumentBase64);
            await _documentBase64Context.SaveChangesAsync();
            return document.Id != 0;
        }

        public async Task<bool> DeleteDocument(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null) return false;

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDocuments(int[] ids)
        {
            var documents = await _context.Documents.Where(d => ids.Contains(d.Id)).ToListAsync();
            if (documents.Count == 0)
            {
                return false;
            }

            _context.Documents.RemoveRange(documents);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<string?> GetBase64(int id)
        {
            var relation = await _documentBase64Context.Document_Base64.FindAsync(id);
            return relation?.Base64;
        }

        public async Task<bool> UpdateSize(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null) return false;

            document.size = 0;
            _context.Documents.Update(document);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Document?> GetDocument(int id)
        {
            return await _context.Documents.FindAsync(id);
        }
    }
}

